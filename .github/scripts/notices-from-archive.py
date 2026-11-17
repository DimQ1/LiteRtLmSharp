"""Reproduce the upstream license bundle from a C API zip or older Apple xcframework.

C API 1.0.0 ships LICENSE + licenses/*; older releases ship the component licenses
inside the ios-arm64 framework instead.

Usage: notices-from-archive.py <archive.zip> <output.txt> <upstream tag>
"""
import html
import re
import sys
import zipfile


def tidy(text):
    """Drops surrounding blank lines and trailing spaces but keeps the indentation of centered titles."""
    return text.strip("\n").rstrip()


src, out, tag = sys.argv[1:4]
with zipfile.ZipFile(src) as z:
    names = [n for n in z.namelist() if not n.startswith("__MACOSX")]
    if "LICENSE" in names and any(n.startswith("licenses/") for n in names):
        license_path = "LICENSE"
        bundle = "licenses/"
        components = sorted(n for n in names if n.startswith(bundle) and not n.endswith("/"))
        origin = "the official C API archive's licenses directory"
    else:
        license_path = next(
            (n for n in names if n.endswith("/ios-arm64/CLiteRTLM.framework/LICENSE")), None
        )
        if license_path is None:
            sys.exit(f"no upstream LICENSE found in {src}")
        framework = license_path.rsplit("/", 1)[0]
        bundle = framework + "/third_party_licenses.bundle/"
        components = sorted(n for n in names if n.startswith(bundle) and n.endswith("_LICENSE.html"))
        origin = "the official CLiteRTLM.xcframework third_party_licenses.bundle"
    if not components:
        sys.exit(f"no third-party license components in {src}")

    rule = "=" * 80
    parts = [
        f"{rule}\nTHIRD-PARTY SOFTWARE NOTICES AND INFORMATION\nLiteRT-LM {tag} native libraries\n{rule}\n\n"
        "This file reproduces the license of LiteRT-LM and the license of each third-party component that\n"
        f"Google lists in {origin} of LiteRT-LM {tag}.\n"
        f"{len(components)} third-party components follow the LiteRT-LM license.\n",
        f"{rule}\nLiteRT-LM\n{rule}\n\n{tidy(z.read(license_path).decode('utf-8'))}\n",
    ]
    for name in components:
        component = name[len(bundle):].removesuffix("_LICENSE.html").removesuffix(".html")
        page = z.read(name).decode("utf-8")
        if name.endswith(".html"):
            match = re.search(r"<pre(?:\s[^>]*)?>(.*?)</pre>", page, re.S | re.I)
            text = html.unescape(match.group(1) if match else re.sub(r"<[^>]+>", "", page))
        else:
            text = page
        parts.append(f"{rule}\n{component}\n{rule}\n\n{tidy(text)}\n")

with open(out, "w", encoding="utf-8", newline="\n") as f:
    f.write("\n".join(parts))
print(f"wrote {out}: LiteRT-LM license + {len(components)} third-party components")
