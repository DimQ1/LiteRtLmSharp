#!/usr/bin/env python3
"""Verify the bound C ABI against the headers shipped with a native release."""

import argparse
import pathlib
import re
import tempfile
import urllib.request


def parameters(text):
    result, depth, start = [], 0, 0
    for i, char in enumerate(text):
        if char in "<([":
            depth += 1
        elif char in ">)]":
            depth -= 1
        elif char == "," and depth == 0:
            result.append(text[start:i].strip())
            start = i + 1
    tail = text[start:].strip()
    if tail and tail != "void":
        result.append(tail)
    return result


def kind(declaration, c=False):
    declaration = re.sub(r"\[MarshalAs\([^]]+\)\]\s*", "", declaration)
    declaration = re.sub(r"\bconst\b", "", declaration).strip()
    value_type = declaration.rsplit(" ", 1)[0].strip()
    value_type = re.sub(r"\s+", "", value_type)
    if "delegate*" in value_type:
        if "unmanaged[Cdecl]<nint,nint,void>" not in value_type:
            raise ValueError(f"Unexpected callback convention: {declaration}")
        return "callback"
    if c and value_type == "LiteRtLmStreamCallback":
        return "callback"
    aliases = {
        "size_t": "nuint", "int32_t": "int", "uint32_t": "uint",
        "int64_t": "long", "uint64_t": "ulong", "bool": "byte",
    }
    stars = len(value_type) - len(value_type.rstrip("*"))
    base = value_type.rstrip("*")
    if base in ("nint", "string", "string?"):
        return "pointer" + "*" * stars
    base = aliases.get(base, base)
    if stars >= 2:
        return "pointer" + "*" * (stars - 1)
    if base == "byte" and stars:
        return "pointer" + "*" * (stars - 1)
    if base in ("void", "char") and stars:
        return "pointer" + "*" * (stars - 1)
    if c and base.startswith("LiteRtLm") and stars and base not in ENUMS:
        return "pointer" + "*" * (stars - 1)
    if base.startswith("LiteRtLm") or base == "LiteRtStatusCode":
        base = "int"
    return aliases.get(base, base) + "*" * stars


parser = argparse.ArgumentParser(description=__doc__)
parser.add_argument("headers", nargs="?", type=pathlib.Path)
parser.add_argument("--ref", help="Download headers from this pinned LiteRT-LM tag")
args = parser.parse_args()
download = tempfile.TemporaryDirectory() if args.ref else None
if args.ref:
    if not re.fullmatch(r"v\d+\.\d+\.\d+", args.ref):
        parser.error("--ref must be a release tag, for example v0.19.1")
    args.headers = pathlib.Path(download.name)
    for name in ("engine", "conversation", "embedding_engine", "model_info", "error_reporter", "experimental"):
        url = f"https://raw.githubusercontent.com/google-ai-edge/LiteRT-LM/{args.ref}/c/{name}.h"
        urllib.request.urlretrieve(url, args.headers / f"{name}.h")
if args.headers is None:
    parser.error("Pass a headers directory or --ref")
text = "\n".join(p.read_text(encoding="utf-8") for p in args.headers.glob("*.h"))
text = re.sub(r"//[^\n]*|/\*.*?\*/", "", text, flags=re.S)
text = re.sub(r"LITERT_LM_C_API_DEPRECATED\(.*?\)", "", text, flags=re.S)
ENUMS = set(re.findall(r"typedef enum(?:\s+\w+)?\s*\{.*?\}\s*(\w+)\s*;", text, re.S))
exports = {
    name: (ret.strip(), parameters(params))
    for ret, name, params in re.findall(
        r"LITERT_LM_C_API_EXPORT\s+([\w\s*]+?)\s+(litert_lm_\w+)\s*\((.*?)\)\s*;",
        text, re.S,
    )
}
root = pathlib.Path(__file__).resolve().parents[1]
errors, count, imports = [], 0, 0
for source in (root / "LiteRtLmSharp" / "Native").glob("LiteRtLmNative*.cs"):
    source_text = source.read_text(encoding="utf-8-sig")
    imports += source_text.count("[LibraryImport(")
    for attributes, ret, managed, params in re.findall(
        r"((?:    \[[^\n]*\]\s*\n)+)    (?:internal|private) static partial (\S+) (\w+)\((.*?)\);",
        source_text, re.S,
    ):
        entry = re.search(r'EntryPoint = "(\w+)"', attributes)
        name = entry[1] if entry else managed
        if name not in exports:
            errors.append(f"{name}: not exported by the pinned headers")
            continue
        native_ret, native_params = exports[name]
        actual = [kind(ret + " value")] + [kind(p) for p in parameters(params)]
        expected = [kind(native_ret + " value", c=True)] + [kind(p, c=True) for p in native_params]
        if actual != expected:
            errors.append(f"{name}: C# {actual}, native {expected}")
        count += 1
if count < 160:
    errors.append(f"Only {count} imports checked; declaration parsing is incomplete")
if count != imports:
    errors.append(f"Parsed {count} declarations but found {imports} LibraryImport attributes")
if errors:
    raise SystemExit("\n".join(errors))
print(f"Verified {count} native signatures against {len(exports)} exports (including size_t and Cdecl).")
if download:
    download.cleanup()
