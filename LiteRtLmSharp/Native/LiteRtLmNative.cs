using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LiteRtLmSharp.Native;

/// <summary>
/// Checked adapters and raw P/Invoke declarations for LiteRT-LM C API 1.0.0.
/// Source-generated marshalling via <see cref="LibraryImportAttribute"/> (AOT-friendly).
/// String parameters are UTF-8; <c>const char*</c> returns are owned by native objects
/// and are returned as <see cref="nint"/> to avoid the runtime freeing them.
/// </summary>
internal static unsafe partial class LiteRtLmNative
{
    internal const string Library = "LiteRtLm";

    // --- Logging ---------------------------------------------------------

    internal static void litert_lm_set_min_log_level(int level)
    {
        int status = V1_litert_lm_set_min_log_level(level);
        NativeError.Check(status, "litert_lm_set_min_log_level");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_set_min_log_level")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial int V1_litert_lm_set_min_log_level(
        int level);

    // --- Error reporting (v0.18.0, c/error_reporter.h) --------------------
    // errno-style and thread-local: a FAILING call stores a canonical status code and a message on the
    // calling thread; successful calls never clear them. NativeError owns the read/clear discipline.

    /// <summary>The calling thread's last failure status (absl canonical code), or 0 when none is
    /// recorded.</summary>
    internal static int litert_lm_get_last_error_code() => NativeError.LastStatus;

    /// <summary>The calling thread's last failure message, or null. Owned by the library (thread-local)
    /// and valid until the next failing call or clear on that thread — copy it out immediately.</summary>
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nint litert_lm_get_last_error_message();

    /// <summary>Resets the calling thread's error state (code 0, message null) and frees the message.</summary>
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_clear_last_error();

    // --- Engine settings -------------------------------------------------

    internal static nint litert_lm_engine_settings_create(string model_path, string backend_str, string? vision_backend_str, string? audio_backend_str)
    {
        nint result = default;
        int status = V1_litert_lm_engine_settings_create(model_path, backend_str, vision_backend_str, audio_backend_str, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_create", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_create(
        string model_path, string backend_str, string? vision_backend_str, string? audio_backend_str, nint* out_result);

    internal static void litert_lm_engine_settings_set_max_num_tokens(nint settings, int max_num_tokens)
    {
        int status = V1_litert_lm_engine_settings_set_max_num_tokens(settings, max_num_tokens);
        NativeError.Check(status, "litert_lm_engine_settings_set_max_num_tokens");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_max_num_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_max_num_tokens(
        nint settings, int max_num_tokens);

    /// <summary>Number of threads for the CPU text executor. A no-op on non-CPU backends: the setter
    /// fetches the executor's <c>CpuConfig</c> and only applies when the backend is CPU (see engine.cc
    /// <c>set_num_threads</c>).</summary>
    internal static void litert_lm_engine_settings_set_num_threads(nint settings, int num_threads)
    {
        int status = V1_litert_lm_engine_settings_set_num_threads(settings, num_threads);
        NativeError.Check(status, "litert_lm_engine_settings_set_num_threads");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_num_threads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_num_threads(
        nint settings, int num_threads);

    /// <summary>Number of threads for the CPU audio executor. Applies only when an audio executor is
    /// configured, and only reaches the audio backend's CPU compilation options (audio_executor path).</summary>
    internal static void litert_lm_engine_settings_set_audio_num_threads(nint settings, int num_threads)
    {
        int status = V1_litert_lm_engine_settings_set_audio_num_threads(settings, num_threads);
        NativeError.Check(status, "litert_lm_engine_settings_set_audio_num_threads");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_audio_num_threads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_audio_num_threads(
        nint settings, int num_threads);

    /// <summary>Sets the LoRA rank for the text executor. 0 disables LoRA. Requires a LoRA-enabled model.</summary>
    internal static void litert_lm_engine_settings_set_lora_rank(nint settings, int lora_rank)
    {
        int status = V1_litert_lm_engine_settings_set_lora_rank(settings, lora_rank);
        NativeError.Check(status, "litert_lm_engine_settings_set_lora_rank");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_lora_rank")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_lora_rank(
        nint settings, int lora_rank);

    /// <summary>Sets the supported LoRA ranks for the text executor. Returns 0 on success, non-zero on
    /// null/empty input. The ranks are only honored on the GPU (Artisan) backend; on other backends the
    /// native layer accepts and ignores them (still returns success) — see engine.cc + llm_executor_settings.h.</summary>
    internal static int litert_lm_engine_settings_set_supported_lora_ranks(nint settings, int* lora_ranks, nuint num_ranks)
    {
        int status = V1_litert_lm_engine_settings_set_supported_lora_ranks(settings, lora_ranks, num_ranks);
        NativeError.Record(status);
        return status;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_supported_lora_ranks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_supported_lora_ranks(
        nint settings, int* lora_ranks, nuint num_ranks);

    /// <summary>Sets the LoRA rank for the audio executor. Applies only when an audio executor is configured.</summary>
    internal static void litert_lm_engine_settings_set_audio_lora_rank(nint settings, int lora_rank)
    {
        int status = V1_litert_lm_engine_settings_set_audio_lora_rank(settings, lora_rank);
        NativeError.Check(status, "litert_lm_engine_settings_set_audio_lora_rank");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_audio_lora_rank")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_audio_lora_rank(
        nint settings, int lora_rank);

    /// <summary>Sets the supported LoRA ranks for the audio executor. Returns 0 on success, non-zero on
    /// null/empty input or when no audio executor is configured.</summary>
    internal static int litert_lm_engine_settings_set_supported_audio_lora_ranks(nint settings, int* lora_ranks, nuint num_ranks)
    {
        int status = V1_litert_lm_engine_settings_set_supported_audio_lora_ranks(settings, lora_ranks, num_ranks);
        NativeError.Record(status);
        return status;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_supported_audio_lora_ranks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_supported_audio_lora_ranks(
        nint settings, int* lora_ranks, nuint num_ranks);

    /// <summary>Sets the maximum number of images the engine accepts. Per the header this only
    /// affects the <i>legacy</i> engine implementation; the current path ignores it. Bound for
    /// completeness — the per-conversation knob is <c>optional_args_set_visual_token_budget</c>.</summary>
    internal static void litert_lm_engine_settings_set_max_num_images(nint settings, int max_num_images)
    {
        int status = V1_litert_lm_engine_settings_set_max_num_images(settings, max_num_images);
        NativeError.Check(status, "litert_lm_engine_settings_set_max_num_images");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_max_num_images")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_max_num_images(
        nint settings, int max_num_images);

    /// <summary>Caps the vision tokens generated per image: the engine picks the smallest vision
    /// encoder/adapter signature that fits (engine_advanced_impl.cc). Must be positive; only applies when
    /// a vision executor is configured and the model declares vision tokens per image. Per-send visual
    /// token budgets above it are then rejected (conversation.cc ValidateVisualTokenBudget) (v0.18.0+).</summary>
    internal static void litert_lm_engine_settings_set_max_vision_tokens_per_image(nint settings, int max_vision_tokens_per_image)
    {
        int status = V1_litert_lm_engine_settings_set_max_vision_tokens_per_image(settings, max_vision_tokens_per_image);
        NativeError.Check(status, "litert_lm_engine_settings_set_max_vision_tokens_per_image");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_max_vision_tokens_per_image")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_max_vision_tokens_per_image(
        nint settings, int max_vision_tokens_per_image);

    /// <summary>Keeps model weights and allocations resident in GPU memory through Apple's MTLResidencySet
    /// API. Apple platforms with the Metal GPU backend only; ignored elsewhere (v0.18.0+).</summary>
    internal static void litert_lm_engine_settings_set_gpu_enable_metal_residency_set(nint settings, bool enable_metal_residency_set)
    {
        int status = V1_litert_lm_engine_settings_set_gpu_enable_metal_residency_set(settings, enable_metal_residency_set);
        NativeError.Check(status, "litert_lm_engine_settings_set_gpu_enable_metal_residency_set");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_gpu_enable_metal_residency_set")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_gpu_enable_metal_residency_set(
        nint settings, [MarshalAs(UnmanagedType.U1)] bool enable_metal_residency_set);

    /// <summary>Enables speculative decoding (MTP drafter); requires a model that ships a drafter.</summary>
    internal static void litert_lm_engine_settings_set_enable_speculative_decoding(nint settings, bool enable)
    {
        int status = V1_litert_lm_engine_settings_set_enable_speculative_decoding(settings, enable);
        NativeError.Check(status, "litert_lm_engine_settings_set_enable_speculative_decoding");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_enable_speculative_decoding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_enable_speculative_decoding(
        nint settings, [MarshalAs(UnmanagedType.U1)] bool enable);

    /// <summary>Turns on benchmark instrumentation so the conversation exposes timings.</summary>
    internal static void litert_lm_engine_settings_enable_benchmark(nint settings)
    {
        int status = V1_litert_lm_engine_settings_enable_benchmark(settings);
        NativeError.Check(status, "litert_lm_engine_settings_enable_benchmark");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_enable_benchmark")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_enable_benchmark(
        nint settings);

    /// <summary>Directory for the compiled-artifact cache. Special values <c>:nocache</c> (disable)
    /// and <c>:memory</c> (in-RAM); empty = next to the model file.</summary>
    internal static void litert_lm_engine_settings_set_cache_dir(nint settings, string cache_dir)
    {
        int status = V1_litert_lm_engine_settings_set_cache_dir(settings, cache_dir);
        NativeError.Check(status, "litert_lm_engine_settings_set_cache_dir");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_cache_dir", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_cache_dir(
        nint settings, string cache_dir);

    /// <summary>Whether to load the <c>.litertlm</c> file sections in parallel. Defaults to true.</summary>
    internal static void litert_lm_engine_settings_set_parallel_file_section_loading(nint settings, bool parallel_file_section_loading)
    {
        int status = V1_litert_lm_engine_settings_set_parallel_file_section_loading(settings, parallel_file_section_loading);
        NativeError.Check(status, "litert_lm_engine_settings_set_parallel_file_section_loading");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_parallel_file_section_loading")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_parallel_file_section_loading(
        nint settings, [MarshalAs(UnmanagedType.U1)] bool parallel_file_section_loading);

    /// <summary>Whether YNNPACK delegates supported CPU operations before XNNPACK (experimental). Native v0.16.0+.</summary>
    internal static void litert_lm_engine_settings_set_enable_ynnpack(nint settings, bool enable_ynnpack)
    {
        int status = V1_litert_lm_engine_settings_set_enable_ynnpack(settings, enable_ynnpack);
        NativeError.Check(status, "litert_lm_engine_settings_set_enable_ynnpack");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_enable_ynnpack")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_enable_ynnpack(
        nint settings, [MarshalAs(UnmanagedType.U1)] bool enable_ynnpack);

    /// <summary>Activation tensor precision (0=F32, 1=F16, 2=I16, 3=I8 per <c>ActivationDataType</c>).</summary>
    internal static void litert_lm_engine_settings_set_activation_data_type(nint settings, int activation_data_type_int)
    {
        int status = V1_litert_lm_engine_settings_set_activation_data_type(settings, activation_data_type_int);
        NativeError.Check(status, "litert_lm_engine_settings_set_activation_data_type");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_activation_data_type")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_activation_data_type(
        nint settings, int activation_data_type_int);

    /// <summary>Prefill chunk size. Only applicable to the CPU backend with dynamic models.</summary>
    internal static void litert_lm_engine_settings_set_prefill_chunk_size(nint settings, int prefill_chunk_size)
    {
        int status = V1_litert_lm_engine_settings_set_prefill_chunk_size(settings, prefill_chunk_size);
        NativeError.Check(status, "litert_lm_engine_settings_set_prefill_chunk_size");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_prefill_chunk_size")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_prefill_chunk_size(
        nint settings, int prefill_chunk_size);

    /// <summary>Number of synthetic prefill tokens for benchmarking (read by the benchmark path).</summary>
    internal static void litert_lm_engine_settings_set_num_prefill_tokens(nint settings, int num_prefill_tokens)
    {
        int status = V1_litert_lm_engine_settings_set_num_prefill_tokens(settings, num_prefill_tokens);
        NativeError.Check(status, "litert_lm_engine_settings_set_num_prefill_tokens");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_num_prefill_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_num_prefill_tokens(
        nint settings, int num_prefill_tokens);

    /// <summary>Number of synthetic decode tokens for benchmarking (read by the benchmark path).</summary>
    internal static void litert_lm_engine_settings_set_num_decode_tokens(nint settings, int num_decode_tokens)
    {
        int status = V1_litert_lm_engine_settings_set_num_decode_tokens(settings, num_decode_tokens);
        NativeError.Check(status, "litert_lm_engine_settings_set_num_decode_tokens");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_num_decode_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_num_decode_tokens(
        nint settings, int num_decode_tokens);

    /// <summary>Decode steps per GPU sync. Only honored by the Artisan GPU backend (v0.15.0+).</summary>
    internal static void litert_lm_engine_settings_set_gpu_decode_steps_per_sync(nint settings, int num_decode_steps_per_sync)
    {
        int status = V1_litert_lm_engine_settings_set_gpu_decode_steps_per_sync(settings, num_decode_steps_per_sync);
        NativeError.Check(status, "litert_lm_engine_settings_set_gpu_decode_steps_per_sync");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_gpu_decode_steps_per_sync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_gpu_decode_steps_per_sync(
        nint settings, int num_decode_steps_per_sync);

    /// <summary>Whether to wait for GPU weight uploads. Only honored by the Artisan GPU backend (v0.15.0+).</summary>
    internal static void litert_lm_engine_settings_set_gpu_wait_for_weight_uploads(nint settings, bool wait_for_weight_uploads)
    {
        int status = V1_litert_lm_engine_settings_set_gpu_wait_for_weight_uploads(settings, wait_for_weight_uploads);
        NativeError.Check(status, "litert_lm_engine_settings_set_gpu_wait_for_weight_uploads");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_gpu_wait_for_weight_uploads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_gpu_wait_for_weight_uploads(
        nint settings, [MarshalAs(UnmanagedType.U1)] bool wait_for_weight_uploads);

    /// <summary>Ringbuffer KV cache for local-attention layers (lower memory, no instant rewind).
    /// Backend-agnostic in interface but currently only the Artisan GPU backend implements it;
    /// unsupported models/backends log a warning and ignore it (v0.15.0+).</summary>
    internal static void litert_lm_engine_settings_set_use_ringbuffers_local_attention(nint settings, bool use_ringbuffers_local_attention)
    {
        int status = V1_litert_lm_engine_settings_set_use_ringbuffers_local_attention(settings, use_ringbuffers_local_attention);
        NativeError.Check(status, "litert_lm_engine_settings_set_use_ringbuffers_local_attention");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_settings_set_use_ringbuffers_local_attention")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_settings_set_use_ringbuffers_local_attention(
        nint settings, [MarshalAs(UnmanagedType.U1)] bool use_ringbuffers_local_attention);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_engine_settings_delete(nint settings);

    // --- Engine ----------------------------------------------------------

    internal static nint litert_lm_engine_create(nint settings)
    {
        nint result = default;
        int status = V1_litert_lm_engine_create(settings, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_create(
        nint settings, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_engine_delete(nint engine);

    // --- Sampler params (v0.14.0 opaque builder) ------------------------
    // v0.14.0 replaced the by-value LiteRtLmSamplerParams struct with an opaque object built through
    // setters. Create one, set all fields, pass it to session_config_set_sampler_params (which COPIES
    // the fields), then delete it — the caller retains ownership. create() zeroes all numeric fields
    // (NOT the ecosystem defaults), so the binding always sets all four explicitly.

    /// <summary>Allocates an opaque sampler-params object of the given type (all numeric fields zeroed).
    /// Caller frees it with <see cref="litert_lm_sampler_params_delete"/>. Null on failure.</summary>
    internal static nint litert_lm_sampler_params_create(LiteRtLmSamplerType type)
    {
        nint result = default;
        int status = V1_litert_lm_sampler_params_create(type, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_sampler_params_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_sampler_params_create(
        LiteRtLmSamplerType type, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_sampler_params_delete(nint sampler_params);

    internal static void litert_lm_sampler_params_set_top_k(nint sampler_params, int top_k)
    {
        int status = V1_litert_lm_sampler_params_set_top_k(sampler_params, top_k);
        NativeError.Check(status, "litert_lm_sampler_params_set_top_k");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_sampler_params_set_top_k")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_sampler_params_set_top_k(
        nint sampler_params, int top_k);

    internal static void litert_lm_sampler_params_set_top_p(nint sampler_params, float top_p)
    {
        int status = V1_litert_lm_sampler_params_set_top_p(sampler_params, top_p);
        NativeError.Check(status, "litert_lm_sampler_params_set_top_p");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_sampler_params_set_top_p")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_sampler_params_set_top_p(
        nint sampler_params, float top_p);

    internal static void litert_lm_sampler_params_set_temperature(nint sampler_params, float temperature)
    {
        int status = V1_litert_lm_sampler_params_set_temperature(sampler_params, temperature);
        NativeError.Check(status, "litert_lm_sampler_params_set_temperature");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_sampler_params_set_temperature")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_sampler_params_set_temperature(
        nint sampler_params, float temperature);

    internal static void litert_lm_sampler_params_set_seed(nint sampler_params, int seed)
    {
        int status = V1_litert_lm_sampler_params_set_seed(sampler_params, seed);
        NativeError.Check(status, "litert_lm_sampler_params_set_seed");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_sampler_params_set_seed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_sampler_params_set_seed(
        nint sampler_params, int seed);

    // --- Session config -------------------------------------------------

    internal static nint litert_lm_session_config_create()
    {
        nint result = default;
        int status = V1_litert_lm_session_config_create(&result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_session_config_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_session_config_create(
        nint* out_result);

    internal static void litert_lm_session_config_set_max_output_tokens(nint config, int max_output_tokens)
    {
        int status = V1_litert_lm_session_config_set_max_output_tokens(config, max_output_tokens);
        NativeError.Check(status, "litert_lm_session_config_set_max_output_tokens");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_session_config_set_max_output_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_session_config_set_max_output_tokens(
        nint config, int max_output_tokens);

    /// <summary>Copies the opaque sampler params into the session config; the caller keeps ownership of
    /// <paramref name="sampler_params"/> and deletes it afterwards.</summary>
    internal static void litert_lm_session_config_set_sampler_params(nint config, nint sampler_params)
    {
        int status = V1_litert_lm_session_config_set_sampler_params(config, sampler_params);
        NativeError.Check(status, "litert_lm_session_config_set_sampler_params");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_session_config_set_sampler_params")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_session_config_set_sampler_params(
        nint config, nint sampler_params);

    /// <summary>Sets the text LoRA weights file for the session. The native side OPENS the file
    /// immediately (validates it exists/opens), so a bad path fails here. Returns 0 on success, non-zero
    /// on a null/empty path or an open failure. Requires a LoRA-enabled model.</summary>
    internal static int litert_lm_session_config_set_lora_path(nint config, string lora_path)
    {
        int status = V1_litert_lm_session_config_set_lora_path(config, lora_path);
        NativeError.Record(status);
        return status;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_session_config_set_lora_path", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_session_config_set_lora_path(
        nint config, string lora_path);

    /// <summary>Sets the audio LoRA weights file for the session. Opens the file immediately (see
    /// <see cref="litert_lm_session_config_set_lora_path"/>). Returns 0 on success, non-zero on failure.</summary>
    internal static int litert_lm_session_config_set_audio_lora_path(nint config, string audio_lora_path)
    {
        int status = V1_litert_lm_session_config_set_audio_lora_path(config, audio_lora_path);
        NativeError.Record(status);
        return status;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_session_config_set_audio_lora_path", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_session_config_set_audio_lora_path(
        nint config, string audio_lora_path);

    /// <summary>Per-session speculative decoding. Unset = inherit the engine setting; true on an engine
    /// created without it lazily loads the MTP drafter on the session's first request; false disables it
    /// for this session even when the engine enables it (v0.18.0+).</summary>
    internal static void litert_lm_session_config_set_enable_speculative_decoding(nint config, bool enable_speculative_decoding)
    {
        int status = V1_litert_lm_session_config_set_enable_speculative_decoding(config, enable_speculative_decoding);
        NativeError.Check(status, "litert_lm_session_config_set_enable_speculative_decoding");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_session_config_set_enable_speculative_decoding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_session_config_set_enable_speculative_decoding(
        nint config, [MarshalAs(UnmanagedType.U1)] bool enable_speculative_decoding);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_session_config_delete(nint config);

    // --- Conversation config --------------------------------------------

    internal static nint litert_lm_conversation_config_create()
    {
        nint result = default;
        int status = V1_litert_lm_conversation_config_create(&result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_create(
        nint* out_result);

    internal static void litert_lm_conversation_config_set_session_config(nint config, nint session_config)
    {
        int status = V1_litert_lm_conversation_config_set_session_config(config, session_config);
        NativeError.Check(status, "litert_lm_conversation_config_set_session_config");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_session_config")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_session_config(
        nint config, nint session_config);

    internal static void litert_lm_conversation_config_set_system_message(nint config, string system_message_json)
    {
        int status = V1_litert_lm_conversation_config_set_system_message(config, system_message_json);
        NativeError.Check(status, "litert_lm_conversation_config_set_system_message");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_system_message", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_system_message(
        nint config, string system_message_json);

    internal static void litert_lm_conversation_config_set_tools(nint config, string tools_json)
    {
        int status = V1_litert_lm_conversation_config_set_tools(config, tools_json);
        NativeError.Check(status, "litert_lm_conversation_config_set_tools");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_tools", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_tools(
        nint config, string tools_json);

    /// <summary>Sets the initial messages (conversation history / few-shot preface) as a JSON
    /// <b>array</b>. Consumed at <c>conversation_create</c>: parsed and appended to the preface after
    /// the system message, then prefilled into the KV cache.</summary>
    internal static void litert_lm_conversation_config_set_messages(nint config, string messages_json)
    {
        int status = V1_litert_lm_conversation_config_set_messages(config, messages_json);
        NativeError.Check(status, "litert_lm_conversation_config_set_messages");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_messages", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_messages(
        nint config, string messages_json);

    internal static void litert_lm_conversation_config_set_enable_constrained_decoding(nint config, bool enable)
    {
        int status = V1_litert_lm_conversation_config_set_enable_constrained_decoding(config, enable);
        NativeError.Check(status, "litert_lm_conversation_config_set_enable_constrained_decoding");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_enable_constrained_decoding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_enable_constrained_decoding(
        nint config, [MarshalAs(UnmanagedType.U1)] bool enable);

    /// <summary>Sets the conversation-preface extra context (a JSON object string) passed to the
    /// prompt-template renderer, e.g. <c>{"enable_thinking":true}</c> for Gemma reasoning mode.</summary>
    internal static void litert_lm_conversation_config_set_extra_context(nint config, string extra_context_json)
    {
        int status = V1_litert_lm_conversation_config_set_extra_context(config, extra_context_json);
        NativeError.Check(status, "litert_lm_conversation_config_set_extra_context");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_extra_context", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_extra_context(
        nint config, string extra_context_json);

    /// <summary>Whether to drop channel content (in practice the thinking channel) from the KV cache,
    /// so a long reasoning block does not consume the context window on later turns.</summary>
    internal static void litert_lm_conversation_config_set_filter_channel_content_from_kv_cache(nint config, bool filter)
    {
        int status = V1_litert_lm_conversation_config_set_filter_channel_content_from_kv_cache(config, filter);
        NativeError.Check(status, "litert_lm_conversation_config_set_filter_channel_content_from_kv_cache");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_filter_channel_content_from_kv_cache")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_filter_channel_content_from_kv_cache(
        nint config, [MarshalAs(UnmanagedType.U1)] bool filter);

    /// <summary>Streams the raw tool-call text incrementally on the named channel while it is being
    /// generated (default off: the callback stays silent during a tool-call block and delivers it whole
    /// at the end). The complete tool-call message still arrives as before — the channel chunks are an
    /// additional progress feed (internal_callback_util.cc streams up to a safe cursor that can never
    /// contain a partial end delimiter). Requires native LiteRT-LM v0.14.0+.</summary>
    internal static void litert_lm_conversation_config_set_stream_tool_calls(nint config, bool stream_tool_calls, string channel_name)
    {
        int status = V1_litert_lm_conversation_config_set_stream_tool_calls(config, stream_tool_calls, channel_name);
        NativeError.Check(status, "litert_lm_conversation_config_set_stream_tool_calls");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_stream_tool_calls", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_stream_tool_calls(
        nint config, [MarshalAs(UnmanagedType.U1)] bool stream_tool_calls, string channel_name);

    /// <summary>Overrides the prompt template (e.g. a Jinja template string) for this conversation.
    /// Unset = the model's / engine's default template (v0.15.0+).</summary>
    internal static void litert_lm_conversation_config_set_prompt_template(nint config, string prompt_template)
    {
        int status = V1_litert_lm_conversation_config_set_prompt_template(config, prompt_template);
        NativeError.Check(status, "litert_lm_conversation_config_set_prompt_template");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_prompt_template", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_prompt_template(
        nint config, string prompt_template);

    /// <summary>Copies the opaque thinking config (enable flag + token budget) into the conversation
    /// config; the caller keeps ownership and deletes it afterwards. Null clears (v0.15.0+).</summary>
    internal static void litert_lm_conversation_config_set_thinking_config(nint config, nint thinking_config)
    {
        int status = V1_litert_lm_conversation_config_set_thinking_config(config, thinking_config);
        NativeError.Check(status, "litert_lm_conversation_config_set_thinking_config");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_thinking_config")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_thinking_config(
        nint config, nint thinking_config);

    /// <summary>Selects the constraint provider for custom constrained decoding (LlGuidance). Takes a
    /// POINTER to the enum value so null can unset it. Mutually exclusive with
    /// <see cref="litert_lm_conversation_config_set_enable_constrained_decoding"/> (tool-calling
    /// constrained decoding) per upstream docs (v0.15.0+).</summary>
    internal static void litert_lm_conversation_config_set_constraint_provider(nint config, int* provider_type)
    {
        int status = V1_litert_lm_conversation_config_set_constraint_provider(config, provider_type == null ? 0 : *provider_type);
        NativeError.Check(status, "litert_lm_conversation_config_set_constraint_provider");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_config_set_constraint_provider")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_config_set_constraint_provider(
        nint config, int provider_type);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_conversation_config_delete(nint config);

    // --- Conversation optional args (multimodal) -------------------------
    // Per-send overrides passed as the last argument to send_message / send_message_stream.
    // The only setter the C API exposes is the visual token budget (image prefill budget).

    internal static nint litert_lm_conversation_optional_args_create()
    {
        nint result = default;
        int status = V1_litert_lm_conversation_optional_args_create(&result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_optional_args_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_optional_args_create(
        nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_conversation_optional_args_delete(nint optional_args);

    /// <summary>Sets the visual token budget (number of tokens images may consume during prefill).</summary>
    internal static void litert_lm_conversation_optional_args_set_visual_token_budget(nint optional_args, int visual_token_budget)
    {
        int status = V1_litert_lm_conversation_optional_args_set_visual_token_budget(optional_args, visual_token_budget);
        NativeError.Check(status, "litert_lm_conversation_optional_args_set_visual_token_budget");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_optional_args_set_visual_token_budget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_optional_args_set_visual_token_budget(
        nint optional_args, int visual_token_budget);

    /// <summary>Sets a per-send max output-tokens cap. When present it overrides the session config's
    /// value for that one send (session_advanced.cc resolves
    /// <c>decode_config.GetMaxOutputTokens().value_or(session_config.GetMaxOutputTokens())</c>); when
    /// absent the session config value applies. Same underlying decode knob as
    /// <see cref="litert_lm_session_config_set_max_output_tokens"/>, at per-send granularity.</summary>
    internal static void litert_lm_conversation_optional_args_set_max_output_tokens(nint optional_args, int max_output_tokens)
    {
        int status = V1_litert_lm_conversation_optional_args_set_max_output_tokens(optional_args, max_output_tokens);
        NativeError.Check(status, "litert_lm_conversation_optional_args_set_max_output_tokens");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_optional_args_set_max_output_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_optional_args_set_max_output_tokens(
        nint optional_args, int max_output_tokens);

    /// <summary>Copies the repetition-penalty config into the optional args (deep copy; the caller
    /// keeps ownership of the config). Applies to this one send's decode. Null clears (v0.15.0+).</summary>
    internal static void litert_lm_conversation_optional_args_set_repetition_penalty_config(nint optional_args, nint repetition_penalty_config)
    {
        int status = V1_litert_lm_conversation_optional_args_set_repetition_penalty_config(optional_args, repetition_penalty_config);
        NativeError.Check(status, "litert_lm_conversation_optional_args_set_repetition_penalty_config");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_optional_args_set_repetition_penalty_config")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_optional_args_set_repetition_penalty_config(
        nint optional_args, nint repetition_penalty_config);

    /// <summary>Copies the no-repeat-ngram config into the optional args (deep copy). Applies to this
    /// one send's decode. Null clears (v0.15.0+).</summary>
    internal static void litert_lm_conversation_optional_args_set_no_repeat_ngram_config(nint optional_args, nint no_repeat_ngram_config)
    {
        int status = V1_litert_lm_conversation_optional_args_set_no_repeat_ngram_config(optional_args, no_repeat_ngram_config);
        NativeError.Check(status, "litert_lm_conversation_optional_args_set_no_repeat_ngram_config");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_optional_args_set_no_repeat_ngram_config")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_optional_args_set_no_repeat_ngram_config(
        nint optional_args, nint no_repeat_ngram_config);

    /// <summary>Copies the suppress-tokens config into the optional args (deep copy). Applies to this
    /// one send's decode. Null or an empty inner set clears (v0.15.0+).</summary>
    internal static void litert_lm_conversation_optional_args_set_suppress_tokens_config(nint optional_args, nint suppress_tokens_config)
    {
        int status = V1_litert_lm_conversation_optional_args_set_suppress_tokens_config(optional_args, suppress_tokens_config);
        NativeError.Check(status, "litert_lm_conversation_optional_args_set_suppress_tokens_config");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_optional_args_set_suppress_tokens_config")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_optional_args_set_suppress_tokens_config(
        nint optional_args, nint suppress_tokens_config);

    /// <summary>Copies the thinking config into the optional args for this one send, overriding the
    /// conversation-level thinking config (conversation.cc ResolveThinkingConfig: per-send wins).
    /// Null clears (v0.15.0+).</summary>
    internal static void litert_lm_conversation_optional_args_set_thinking_config(nint optional_args, nint thinking_config)
    {
        int status = V1_litert_lm_conversation_optional_args_set_thinking_config(optional_args, thinking_config);
        NativeError.Check(status, "litert_lm_conversation_optional_args_set_thinking_config");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_optional_args_set_thinking_config")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_optional_args_set_thinking_config(
        nint optional_args, nint thinking_config);

    /// <summary>Sets a per-send output constraint (regex or JSON Schema string) enforced by the
    /// conversation's constraint provider. Only meaningful when the conversation was created with a
    /// constraint provider (LlGuidance) (v0.15.0+).</summary>
    internal static void litert_lm_conversation_optional_args_set_constraint(nint optional_args, LiteRtLmConstraintType constraint_type, string constraint_string)
    {
        int status = V1_litert_lm_conversation_optional_args_set_constraint(optional_args, constraint_type, constraint_string);
        NativeError.Check(status, "litert_lm_conversation_optional_args_set_constraint");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_optional_args_set_constraint", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_optional_args_set_constraint(
        nint optional_args, LiteRtLmConstraintType constraint_type, string constraint_string);

    // --- Decoding configs (v0.15.0 opaque builders) -----------------------
    // Same lifetime pattern as the sampler params: create, set fields, attach to the optional args /
    // conversation config (which deep-copies), then delete immediately — the caller retains ownership.

    /// <summary>Allocates a repetition-penalty config (defaults: repetition 1.0 = off, presence 0.0,
    /// frequency 0.0, window 0 = infinite). Caller frees with
    /// <see cref="litert_lm_repetition_penalty_config_delete"/>. Null on failure.</summary>
    internal static nint litert_lm_repetition_penalty_config_create()
    {
        nint result = default;
        int status = V1_litert_lm_repetition_penalty_config_create(&result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_repetition_penalty_config_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_repetition_penalty_config_create(
        nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_repetition_penalty_config_delete(nint config);

    /// <summary>Multiplicative repetition penalty (HuggingFace style): 1.0 = off; must be >= 1.0
    /// (values below are clamped to 1.0 during execution).</summary>
    internal static void litert_lm_repetition_penalty_config_set_repetition_penalty(nint config, float repetition_penalty)
    {
        int status = V1_litert_lm_repetition_penalty_config_set_repetition_penalty(config, repetition_penalty);
        NativeError.Check(status, "litert_lm_repetition_penalty_config_set_repetition_penalty");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_repetition_penalty_config_set_repetition_penalty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_repetition_penalty_config_set_repetition_penalty(
        nint config, float repetition_penalty);

    /// <summary>Subtractive presence penalty (OpenAI style): applied once if the token appeared in the window.</summary>
    internal static void litert_lm_repetition_penalty_config_set_presence_penalty(nint config, float presence_penalty)
    {
        int status = V1_litert_lm_repetition_penalty_config_set_presence_penalty(config, presence_penalty);
        NativeError.Check(status, "litert_lm_repetition_penalty_config_set_presence_penalty");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_repetition_penalty_config_set_presence_penalty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_repetition_penalty_config_set_presence_penalty(
        nint config, float presence_penalty);

    /// <summary>Subtractive frequency penalty (OpenAI style): scaled by the token's occurrence count in the window.</summary>
    internal static void litert_lm_repetition_penalty_config_set_frequency_penalty(nint config, float frequency_penalty)
    {
        int status = V1_litert_lm_repetition_penalty_config_set_frequency_penalty(config, frequency_penalty);
        NativeError.Check(status, "litert_lm_repetition_penalty_config_set_frequency_penalty");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_repetition_penalty_config_set_frequency_penalty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_repetition_penalty_config_set_frequency_penalty(
        nint config, float frequency_penalty);

    /// <summary>Generated-history window for the penalties. 0 = infinite; negatives clamp to 0.</summary>
    internal static void litert_lm_repetition_penalty_config_set_window_size(nint config, int window_size)
    {
        int status = V1_litert_lm_repetition_penalty_config_set_window_size(config, window_size);
        NativeError.Check(status, "litert_lm_repetition_penalty_config_set_window_size");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_repetition_penalty_config_set_window_size")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_repetition_penalty_config_set_window_size(
        nint config, int window_size);

    /// <summary>Allocates a no-repeat-ngram config. Caller frees with
    /// <see cref="litert_lm_no_repeat_ngram_config_delete"/>. Null on failure.</summary>
    internal static nint litert_lm_no_repeat_ngram_config_create()
    {
        nint result = default;
        int status = V1_litert_lm_no_repeat_ngram_config_create(&result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_no_repeat_ngram_config_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_no_repeat_ngram_config_create(
        nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_no_repeat_ngram_config_delete(nint config);

    /// <summary>Ngram size banned from repeating: completing an already-seen ngram of this size forces
    /// the candidate token's logit to -inf. &lt;= 0 disables.</summary>
    internal static void litert_lm_no_repeat_ngram_config_set_no_repeat_ngram_size(nint config, int no_repeat_ngram_size)
    {
        int status = V1_litert_lm_no_repeat_ngram_config_set_no_repeat_ngram_size(config, no_repeat_ngram_size);
        NativeError.Check(status, "litert_lm_no_repeat_ngram_config_set_no_repeat_ngram_size");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_no_repeat_ngram_config_set_no_repeat_ngram_size")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_no_repeat_ngram_config_set_no_repeat_ngram_size(
        nint config, int no_repeat_ngram_size);

    /// <summary>Generated-history window for ngram tracking. 0 = infinite; clamped up to the ngram size.</summary>
    internal static void litert_lm_no_repeat_ngram_config_set_window_size(nint config, int window_size)
    {
        int status = V1_litert_lm_no_repeat_ngram_config_set_window_size(config, window_size);
        NativeError.Check(status, "litert_lm_no_repeat_ngram_config_set_window_size");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_no_repeat_ngram_config_set_window_size")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_no_repeat_ngram_config_set_window_size(
        nint config, int window_size);

    /// <summary>Allocates a suppress-tokens config. Caller frees with
    /// <see cref="litert_lm_suppress_tokens_config_delete"/>. Null on failure.</summary>
    internal static nint litert_lm_suppress_tokens_config_create()
    {
        nint result = default;
        int status = V1_litert_lm_suppress_tokens_config_create(&result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_suppress_tokens_config_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_suppress_tokens_config_create(
        nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_suppress_tokens_config_delete(nint config);

    /// <summary>Token ids whose logits are forced to -inf on every decode step. Null/0 clears.</summary>
    internal static void litert_lm_suppress_tokens_config_set_suppress_tokens(nint config, int* suppress_tokens, nuint num_tokens)
    {
        int status = V1_litert_lm_suppress_tokens_config_set_suppress_tokens(config, suppress_tokens, num_tokens);
        NativeError.Check(status, "litert_lm_suppress_tokens_config_set_suppress_tokens");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_suppress_tokens_config_set_suppress_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_suppress_tokens_config_set_suppress_tokens(
        nint config, int* suppress_tokens, nuint num_tokens);

    /// <summary>Allocates a thinking config. NOTE the native default ctor is enabled + infinite budget
    /// (-1); the binding always sets both fields explicitly. Caller frees with
    /// <see cref="litert_lm_thinking_config_delete"/>. Null on failure.</summary>
    internal static nint litert_lm_thinking_config_create()
    {
        nint result = default;
        int status = V1_litert_lm_thinking_config_create(&result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_thinking_config_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_thinking_config_create(
        nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_thinking_config_delete(nint config);

    /// <summary>Whether thinking/reasoning generation is enabled (feeds the template's
    /// <c>enable_thinking</c> variable unless explicit extra context already sets it).</summary>
    internal static void litert_lm_thinking_config_set_enable_thinking(nint config, bool enable_thinking)
    {
        int status = V1_litert_lm_thinking_config_set_enable_thinking(config, enable_thinking);
        NativeError.Check(status, "litert_lm_thinking_config_set_enable_thinking");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_thinking_config_set_enable_thinking")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_thinking_config_set_enable_thinking(
        nint config, [MarshalAs(UnmanagedType.U1)] bool enable_thinking);

    /// <summary>Token budget for the thinking block (-1 = infinite; 0 = treated as no budget by the
    /// task layer). Enforced token-by-token against the model's thinking start/end token ids
    /// (runtime/core/tasks.cc).</summary>
    internal static void litert_lm_thinking_config_set_thinking_token_budget(nint config, int thinking_token_budget)
    {
        int status = V1_litert_lm_thinking_config_set_thinking_token_budget(config, thinking_token_budget);
        NativeError.Check(status, "litert_lm_thinking_config_set_thinking_token_budget");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_thinking_config_set_thinking_token_budget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_thinking_config_set_thinking_token_budget(
        nint config, int thinking_token_budget);

    // --- Conversation ----------------------------------------------------

    internal static nint litert_lm_conversation_create(nint engine, nint config)
    {
        nint result = default;
        int status = V1_litert_lm_conversation_create(engine, config, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_create(
        nint engine, nint config, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_conversation_delete(nint conversation);

    /// <summary>Clones a conversation, duplicating its prefilled (KV-cache) state into a new,
    /// independent conversation. Returns null on failure, including when the engine/backend does
    /// not implement cloning (the native layer returns <c>Unimplemented</c>). The caller owns the
    /// returned pointer and frees it with <see cref="litert_lm_conversation_delete"/>.</summary>
    internal static nint litert_lm_conversation_clone(nint conversation)
    {
        nint result = default;
        int status = V1_litert_lm_conversation_clone(conversation, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_clone")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_clone(
        nint conversation, nint* out_result);

    internal static void litert_lm_conversation_cancel_process(nint conversation)
    {
        int status = V1_litert_lm_conversation_cancel_process(conversation);
        NativeError.Check(status, "litert_lm_conversation_cancel_process");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_cancel_process")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_cancel_process(
        nint conversation);

    /// <summary>Tokens currently in the conversation KV cache (prefill + decode). Negative on failure.</summary>
    internal static int litert_lm_conversation_get_token_count(nint conversation)
    {
        nuint result = default;
        int status = V1_litert_lm_conversation_get_token_count(conversation, &result);
        NativeError.Check(status, "litert_lm_conversation_get_token_count");
        return checked((int)result);
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_get_token_count")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_get_token_count(
        nint conversation, nuint* out_result);

    internal static nint litert_lm_conversation_send_message(nint conversation, string message_json, string? extra_context, nint optional_args)
    {
        nint result = default;
        int status = V1_litert_lm_conversation_send_message(conversation, message_json, extra_context, optional_args, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_send_message", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_send_message(
        nint conversation, string message_json, string? extra_context, nint optional_args, nint* out_result);

    /// <summary>Streaming send. <paramref name="callback"/> is an unmanaged function pointer with the
    /// v0.15.0 <c>LiteRtLmStreamCallback</c> shape: <c>(void* callback_data,
    /// const LiteRtLmStreamChunk* chunk)</c>. The chunk object is only valid for the duration of the
    /// call — read it via the <c>litert_lm_stream_chunk_*</c> getters and copy the strings out.
    /// (v0.14.0 passed <c>(callback_data, text, is_final, error_msg)</c> directly; v0.15.0 moved the
    /// same three fields behind the opaque chunk.)</summary>
    internal static int litert_lm_conversation_send_message_stream(nint conversation, string message_json, string? extra_context, nint optional_args, delegate* unmanaged[Cdecl]<nint, nint, void> callback, nint callback_data)
    {
        int status = V1_litert_lm_conversation_send_message_stream(conversation, message_json, extra_context, optional_args, callback, callback_data);
        NativeError.Record(status);
        return status;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_send_message_stream", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_send_message_stream(
        nint conversation, string message_json, string? extra_context, nint optional_args, delegate* unmanaged[Cdecl]<nint, nint, void> callback, nint callback_data);

    // --- Stream chunk (v0.15.0) ------------------------------------------
    // Read-only views into the chunk passed to the stream callback; the returned strings are owned
    // by the chunk and only valid during the callback invocation.

    // The getters copy borrowed chunk values through status/out parameters before the callback returns.

    /// <summary>The chunk's text content (a full message-JSON piece), or null for error/metadata-only
    /// chunks (including every is_final chunk, which carries no text).</summary>
    internal static nint litert_lm_stream_chunk_get_text(nint chunk)
    {
        nint result = default;
        int status = V1_litert_lm_stream_chunk_get_text(chunk, &result);
        NativeError.Check(status, "litert_lm_stream_chunk_get_text");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_stream_chunk_get_text")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_stream_chunk_get_text(
        nint chunk, nint* out_result);

    /// <summary>True on the last chunk of the stream (done, max-tokens, cancelled or error).</summary>
    internal static bool litert_lm_stream_chunk_is_final(nint chunk)
    {
        byte result = default;
        int status = V1_litert_lm_stream_chunk_is_final(chunk, &result);
        NativeError.Check(status, "litert_lm_stream_chunk_is_final");
        return result != 0;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_stream_chunk_is_final")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_stream_chunk_is_final(
        nint chunk, byte* out_result);

    /// <summary>The chunk's error message, or null when there is no error.</summary>
    internal static nint litert_lm_stream_chunk_get_error(nint chunk)
    {
        nint result = default;
        int status = V1_litert_lm_stream_chunk_get_error(chunk, &result);
        NativeError.Check(status, "litert_lm_stream_chunk_get_error");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_stream_chunk_get_error")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_stream_chunk_get_error(
        nint chunk, nint* out_result);

    /// <summary>Renders a message JSON to its templated prompt string (does not send). The returned
    /// pointer is owned by the conversation and valid only until the next render call or the
    /// conversation's deletion — copy it out immediately. Null on failure.</summary>
    internal static nint litert_lm_conversation_render_message_to_string(nint conversation, string message_json)
    {
        nint result = default;
        int status = V1_litert_lm_conversation_render_message_to_string(conversation, message_json, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_render_message_to_string", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_render_message_to_string(
        nint conversation, string message_json, nint* out_result);

    /// <summary>Renders the conversation's preface (system message + tools + history preamble) to its
    /// templated string, without sending. The returned pointer is owned by the conversation (its own
    /// <c>last_rendered_preface</c> buffer, separate from the message-render buffer) and valid only until
    /// the next <b>preface</b> render or the conversation's deletion — copy it out immediately. Null on
    /// failure.</summary>
    internal static nint litert_lm_conversation_render_preface_to_string(nint conversation)
    {
        nint result = default;
        int status = V1_litert_lm_conversation_render_preface_to_string(conversation, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_render_preface_to_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_render_preface_to_string(
        nint conversation, nint* out_result);

    // --- JSON response ---------------------------------------------------

    internal static nint litert_lm_json_response_get_string(nint response)
    {
        nint result = default;
        int status = V1_litert_lm_json_response_get_string(response, &result);
        NativeError.Check(status, "litert_lm_json_response_get_string");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_json_response_get_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_json_response_get_string(
        nint response, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_json_response_delete(nint response);

    // --- Benchmark info --------------------------------------------------
    // Populated only when the engine was created with benchmark enabled
    // (litert_lm_engine_settings_enable_benchmark). Prefill/decode metrics
    // accumulate per turn; the latest turn is index (num_*_turns - 1).

    /// <summary>Returns the benchmark info for the conversation, or null when unavailable.
    /// Caller owns it and must free with <see cref="litert_lm_benchmark_info_delete"/>.</summary>
    internal static nint litert_lm_conversation_get_benchmark_info(nint conversation)
    {
        nint result = default;
        int status = V1_litert_lm_conversation_get_benchmark_info(conversation, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_conversation_get_benchmark_info")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_conversation_get_benchmark_info(
        nint conversation, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_benchmark_info_delete(nint info);

    internal static double litert_lm_benchmark_info_get_time_to_first_token(nint info)
    {
        double result = default;
        int status = V1_litert_lm_benchmark_info_get_time_to_first_token(info, &result);
        NativeError.Check(status, "litert_lm_benchmark_info_get_time_to_first_token");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_benchmark_info_get_time_to_first_token")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_benchmark_info_get_time_to_first_token(
        nint info, double* out_result);

    internal static double litert_lm_benchmark_info_get_total_init_time_in_second(nint info)
    {
        double result = default;
        int status = V1_litert_lm_benchmark_info_get_total_init_time_in_second(info, &result);
        NativeError.Check(status, "litert_lm_benchmark_info_get_total_init_time_in_second");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_benchmark_info_get_total_init_time_in_second")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_benchmark_info_get_total_init_time_in_second(
        nint info, double* out_result);

    internal static int litert_lm_benchmark_info_get_num_prefill_turns(nint info)
    {
        nuint result = default;
        int status = V1_litert_lm_benchmark_info_get_num_prefill_turns(info, &result);
        NativeError.Check(status, "litert_lm_benchmark_info_get_num_prefill_turns");
        return checked((int)result);
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_benchmark_info_get_num_prefill_turns")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_benchmark_info_get_num_prefill_turns(
        nint info, nuint* out_result);

    internal static int litert_lm_benchmark_info_get_num_decode_turns(nint info)
    {
        nuint result = default;
        int status = V1_litert_lm_benchmark_info_get_num_decode_turns(info, &result);
        NativeError.Check(status, "litert_lm_benchmark_info_get_num_decode_turns");
        return checked((int)result);
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_benchmark_info_get_num_decode_turns")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_benchmark_info_get_num_decode_turns(
        nint info, nuint* out_result);

    internal static int litert_lm_benchmark_info_get_prefill_token_count_at(nint info, int index)
    {
        nuint result = default;
        int status = V1_litert_lm_benchmark_info_get_prefill_token_count_at(info, checked((nuint)index), &result);
        NativeError.Check(status, "litert_lm_benchmark_info_get_prefill_token_count_at");
        return checked((int)result);
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_benchmark_info_get_prefill_token_count_at")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_benchmark_info_get_prefill_token_count_at(
        nint info, nuint index, nuint* out_result);

    internal static int litert_lm_benchmark_info_get_decode_token_count_at(nint info, int index)
    {
        nuint result = default;
        int status = V1_litert_lm_benchmark_info_get_decode_token_count_at(info, checked((nuint)index), &result);
        NativeError.Check(status, "litert_lm_benchmark_info_get_decode_token_count_at");
        return checked((int)result);
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_benchmark_info_get_decode_token_count_at")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_benchmark_info_get_decode_token_count_at(
        nint info, nuint index, nuint* out_result);

    internal static double litert_lm_benchmark_info_get_prefill_tokens_per_sec_at(nint info, int index)
    {
        double result = default;
        int status = V1_litert_lm_benchmark_info_get_prefill_tokens_per_sec_at(info, checked((nuint)index), &result);
        NativeError.Check(status, "litert_lm_benchmark_info_get_prefill_tokens_per_sec_at");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_benchmark_info_get_prefill_tokens_per_sec_at")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_benchmark_info_get_prefill_tokens_per_sec_at(
        nint info, nuint index, double* out_result);

    internal static double litert_lm_benchmark_info_get_decode_tokens_per_sec_at(nint info, int index)
    {
        double result = default;
        int status = V1_litert_lm_benchmark_info_get_decode_tokens_per_sec_at(info, checked((nuint)index), &result);
        NativeError.Check(status, "litert_lm_benchmark_info_get_decode_tokens_per_sec_at");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_benchmark_info_get_decode_tokens_per_sec_at")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_benchmark_info_get_decode_tokens_per_sec_at(
        nint info, nuint index, double* out_result);

    // --- Tokenizer -------------------------------------------------------
    // Exact tokenization / detokenization without running inference, plus the model's configured
    // start (BOS) and stop (EOS) tokens. Each tokenize/detokenize/token-union object is caller-owned
    // and freed by its matching *_delete (see the handles in Handles.cs); the const int*/char* getters
    // point INTO the owning object, so callers copy the data out before disposing it.

    /// <summary>Tokenizes UTF-8 <paramref name="text"/> with the engine's tokenizer. Returns a result
    /// object (caller frees with <see cref="litert_lm_tokenize_result_delete"/>), or null on failure.</summary>
    internal static nint litert_lm_engine_tokenize(nint engine, string text)
    {
        nint result = default;
        int status = V1_litert_lm_engine_tokenize(engine, text, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_tokenize", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_tokenize(
        nint engine, string text, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_tokenize_result_delete(nint result);

    /// <summary>Pointer to the result's internal token-id array (valid only while the result lives).</summary>
    internal static nint litert_lm_tokenize_result_get_tokens(nint result)
    {
        nint tokens = default;
        int status = V1_litert_lm_tokenize_result_get_tokens(result, &tokens);
        NativeError.Check(status, "litert_lm_tokenize_result_get_tokens");
        return tokens;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_tokenize_result_get_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_tokenize_result_get_tokens(
        nint result, nint* out_result);

    internal static nuint litert_lm_tokenize_result_get_num_tokens(nint result)
    {
        nuint count = default;
        int status = V1_litert_lm_tokenize_result_get_num_tokens(result, &count);
        NativeError.Check(status, "litert_lm_tokenize_result_get_num_tokens");
        return count;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_tokenize_result_get_num_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_tokenize_result_get_num_tokens(
        nint result, nuint* out_result);

    /// <summary>Detokenizes a token-id array back to UTF-8. Returns a result object (caller frees with
    /// <see cref="litert_lm_detokenize_result_delete"/>), or null on failure.</summary>
    internal static nint litert_lm_engine_detokenize(nint engine, int* tokens, nuint num_tokens)
    {
        nint result = default;
        int status = V1_litert_lm_engine_detokenize(engine, tokens, num_tokens, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_detokenize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_detokenize(
        nint engine, int* tokens, nuint num_tokens, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_detokenize_result_delete(nint result);

    /// <summary>The detokenized UTF-8 string (owned by the result; valid only while it lives).</summary>
    internal static nint litert_lm_detokenize_result_get_string(nint result)
    {
        nint text = default;
        int status = V1_litert_lm_detokenize_result_get_string(result, &text);
        NativeError.Check(status, "litert_lm_detokenize_result_get_string");
        return text;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_detokenize_result_get_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_detokenize_result_get_string(
        nint result, nint* out_result);

    // A TokenUnion is one start/stop token: either a literal string or a sequence of ids (see the type).

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_token_union_delete(nint token_union);

    internal static LiteRtLmTokenUnionType litert_lm_token_union_get_type(nint token_union)
    {
        LiteRtLmTokenUnionType result = default;
        int status = V1_litert_lm_token_union_get_type(token_union, &result);
        NativeError.Check(status, "litert_lm_token_union_get_type");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_token_union_get_type")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_token_union_get_type(
        nint token_union, LiteRtLmTokenUnionType* out_result);

    /// <summary>The string value, or null when the type is not <see cref="LiteRtLmTokenUnionType.String"/>.</summary>
    internal static nint litert_lm_token_union_get_string(nint token_union)
    {
        nint result = default;
        int status = V1_litert_lm_token_union_get_string(token_union, &result);
        NativeError.Check(status, "litert_lm_token_union_get_string");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_token_union_get_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_token_union_get_string(
        nint token_union, nint* out_result);

    /// <summary>Receives the internal id array + count. Returns 0 on success, non-zero when the type is
    /// not <see cref="LiteRtLmTokenUnionType.Ids"/>.</summary>
    internal static int litert_lm_token_union_get_ids(nint token_union, int** out_tokens, nuint* out_num_tokens)
    {
        int status = V1_litert_lm_token_union_get_ids(token_union, out_tokens, out_num_tokens);
        NativeError.Record(status);
        return status;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_token_union_get_ids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_token_union_get_ids(
        nint token_union, int** out_tokens, nuint* out_num_tokens);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_token_unions_delete(nint tokens);

    internal static nuint litert_lm_token_unions_get_num_tokens(nint tokens)
    {
        nuint result = default;
        int status = V1_litert_lm_token_unions_get_num_tokens(tokens, &result);
        NativeError.Check(status, "litert_lm_token_unions_get_num_tokens");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_token_unions_get_num_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_token_unions_get_num_tokens(
        nint tokens, nuint* out_result);

    /// <summary>The token union at <paramref name="index"/>, borrowed from the collection.
    /// Do not delete it separately; copy its contents before the collection is disposed.</summary>
    internal static nint litert_lm_token_unions_get_token_at(nint tokens, nuint index)
    {
        nint result = default;
        int status = V1_litert_lm_token_unions_get_token_at(tokens, index, &result);
        NativeError.Check(status, "litert_lm_token_unions_get_token_at");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_token_unions_get_token_at")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_token_unions_get_token_at(
        nint tokens, nuint index, nint* out_result);

    /// <summary>The model's configured start (BOS) token, or null if none — caller frees with
    /// <see cref="litert_lm_token_union_delete"/>.</summary>
    internal static nint litert_lm_engine_get_start_token(nint engine)
    {
        nint result = default;
        int status = V1_litert_lm_engine_get_start_token(engine, &result);
        NativeError.Check(status, "litert_lm_engine_get_start_token");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_get_start_token")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_get_start_token(
        nint engine, nint* out_result);

    /// <summary>The model's configured stop (EOS) tokens, or null if none — caller frees with
    /// <see cref="litert_lm_token_unions_delete"/>.</summary>
    internal static nint litert_lm_engine_get_stop_tokens(nint engine)
    {
        nint result = default;
        int status = V1_litert_lm_engine_get_stop_tokens(engine, &result);
        NativeError.Check(status, "litert_lm_engine_get_stop_tokens");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_engine_get_stop_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_engine_get_stop_tokens(
        nint engine, nint* out_result);
}

/// <summary>Mirrors <c>LiteRtLmSamplerType</c> in engine.h. v0.14.0 removed the <c>Unspecified = 0</c>
/// member and v0.18.0 restored it (for model metadata that declares no sampler); a public
/// <see cref="LiteRtSamplerType.Unspecified"/> still never reaches the sampler builder — the binding
/// skips setting sampler params entirely so the executor's internal default applies
/// (see <see cref="LiteRtConversation.Create"/>).</summary>
internal enum LiteRtLmSamplerType
{
    /// <summary>Only reported by model metadata (<c>loaded_file_sampler_type</c>); never sent.</summary>
    Unspecified = 0,
    TopK = 1,
    TopP = 2,
    Greedy = 3,
}

/// <summary>Mirrors <c>LiteRtLmTokenUnionType</c> in engine.h: a start/stop token is either a
/// literal string or a sequence of token ids.</summary>
internal enum LiteRtLmTokenUnionType
{
    String = 0,
    Ids = 1,
}

/// <summary>Mirrors <c>LiteRtLmConstraintType</c> in engine.h (v0.15.0): the shape of a per-send
/// output constraint string.</summary>
internal enum LiteRtLmConstraintType
{
    None = 0,
    Regex = 1,
    JsonSchema = 2,
}


