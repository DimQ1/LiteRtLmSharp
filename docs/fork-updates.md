# Updating the DimQ1 fork

This fork publishes NuGet packages to GitHub Packages and mirrors the same files in public GitHub
releases. It does not publish to nuget.org or change the maintainer's `pr/engine-improvements` branch.
Use suffixed versions such as `1.5.0-dimq1.2`, never an upstream stable version for a different build.

## What changed in 1.5.0-dimq1.2

- Windows metadata reads use ASCII short aliases for non-ASCII paths. The runtime mixes UTF-8 file
  inspection and narrow stream reads; passing ANSI or UTF-8 directly fixes only one layer. If the
  volume provides no ASCII alias, the error explicitly asks for an ASCII model-storage path.
- LoRA isolation is fixed in 0.19.1 and covered by model-backed tests, rather than expecting the old
  engine-wide adapter bug.
- Reviewed managed changes through upstream PR 15 commit `05b2d9f`: per-engine multi-engine opt-in,
  sampler mapping, safe engine lifetime, cached counts, cancellation state and connector fixes.
- Official LiteRT-LM **0.19.1**, **C API 1.0.0**. LiteRT is embedded in the native library; replacing a
  separate LiteRT binary is not an upgrade path.
- The new ABI returns status codes and writes values through output parameters. The binding checks
  status codes, native-size counters and the new borrowed stop-token ownership. The native loader
  rejects older ABI binaries before a call can corrupt memory.
- `Snapshot`/`Resume`, `NearGreedy` and `LiteRtSamplerNotSupportedException` are not in the new API.
  Greedy is represented by TopP with TopK=1. A `Clone()` checkpoint is taken **before** a cancellable
  send and restarts the cut turn. It does not retain partial text already shown to the user.
  Applications needing uninterrupted streaming must keep their own text history, interrupted request
  and delivered partial response, and seed a fresh conversation with that history.
- GPU clone retries still require the workaround documented in the conversation-state guide
  (google-ai-edge/LiteRT-LM#4394). VoiceType uses text-history replay instead of this clone path.

## Publish a future version

1. Change `Version`, `LiteRtLmVersion` and release notes in `Directory.Build.props`. Add a matching
   CHANGELOG section. Keep the native ref in `ci.yml` and `model-tests.yml` consistent.
2. Audit the pinned upstream headers, including ownership changes, not just exported names. Run
   `python scripts/check-capi.py <directory-containing-upstream-c-headers>`. A same-named export can
   still have a different ABI.
3. Run **Native release** on this fork with the native tag, `source=auto`, the release's C API archive
   version (`1.0.0` for 0.19.1), and `publish_release=true`. This verifies upstream asset digests,
   inspects binaries and publishes `native-<tag>` plus checksums/notices. The fork's repository variable
   `NATIVE_ASSETS_REPO` must be `DimQ1/LiteRtLmSharp`.
4. `pwsh scripts/restore-natives.ps1` reads the pinned version automatically. Build and run the
   model-free tests, then model-backed tests with `LITERTLM_TEST_MODEL` and a hang timeout.
   Run the four-platform CI and NativeAOT check on the exact commit to publish.
5. Dispatch **Publish to GitHub Packages** on that commit's branch. Leave version and native source
   empty to use the pinned values and this repository. It packs matching managed/runtime packages,
   tests a clean consumer restore and native call, pushes with the workflow token and mirrors the
   `.nupkg` files in `v<version>`. Suffix versions are marked prereleases.
6. In VoiceType change its one package-version property, fetch release packages, restore and test the
   application. Validate live translation during a chat, repeated interruptions, follow-up state,
   model reload and actual native startup before shipping.

GitHub Packages requires `read:packages` to restore. The public release mirror is the token-free folder
feed alternative. Neither approach requires committing credentials or native/model binaries.

Physical Android/iOS GPU behavior requires device testing; desktop CI and binary inspection do not
establish it. A native package containing private GPU symbols cannot be validated by searching its
dynamic export table: C API 1.0.0 deliberately exports only `litert_lm_*`.
