using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LiteRtLmSharp.Native;

// Embedding engine (c/embedding_engine.h, native v0.18.0) and the generic input-data helpers it consumes
// (c/engine.h). The embedding engine is a separate native object from LiteRtLmEngine: it loads an
// embedding model (EmbeddingGemma 2) and is not subject to the one-live-engine rule.
internal static unsafe partial class LiteRtLmNative
{
    // --- Input data (engine.h) ------------------------------------------

    /// <summary>Creates an input item of the given type. For text, <paramref name="data"/> is UTF-8 and
    /// <paramref name="size"/> its byte length; for image/audio, the raw bytes. The data is copied, so the
    /// caller's buffer can be released right after the call. Caller frees with
    /// <see cref="litert_lm_input_data_delete"/>. Null on failure.</summary>
    internal static nint litert_lm_input_data_create(LiteRtLmInputDataType type, void* data, nuint size)
    {
        nint result = default;
        int status = V1_litert_lm_input_data_create(type, data, size, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_input_data_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_input_data_create(
        LiteRtLmInputDataType type, void* data, nuint size, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_input_data_delete(nint input_data);

    // --- Embedding engine settings ---------------------------------------

    /// <summary>Creates embedding-engine settings for a model file and backend ("cpu", "gpu", "npu"). Null
    /// vision/audio backends mean "the main backend for any encoder the bundle carries": a multimodal
    /// bundle still loads its vision and audio encoders. Null on failure.</summary>
    internal static nint litert_lm_embedding_engine_settings_create(string model_path, string backend_str, string? vision_backend_str, string? audio_backend_str)
    {
        nint result = default;
        int status = V1_litert_lm_embedding_engine_settings_create(model_path, backend_str, vision_backend_str, audio_backend_str, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_settings_create", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_settings_create(
        string model_path, string backend_str, string? vision_backend_str, string? audio_backend_str, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_embedding_engine_settings_delete(nint settings);

    /// <summary>CPU backend thread count.</summary>
    internal static void litert_lm_embedding_engine_settings_set_num_threads(nint settings, int num_threads)
    {
        int status = V1_litert_lm_embedding_engine_settings_set_num_threads(settings, num_threads);
        NativeError.Check(status, "litert_lm_embedding_engine_settings_set_num_threads");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_settings_set_num_threads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_settings_set_num_threads(
        nint settings, int num_threads);

    /// <summary>Cache directory, with the same special values as the LLM engine (<c>:nocache</c>,
    /// <c>:memory</c>). A directory that does not exist makes engine creation fail.</summary>
    internal static void litert_lm_embedding_engine_settings_set_cache_dir(nint settings, string cache_dir)
    {
        int status = V1_litert_lm_embedding_engine_settings_set_cache_dir(settings, cache_dir);
        NativeError.Check(status, "litert_lm_embedding_engine_settings_set_cache_dir");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_settings_set_cache_dir", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_settings_set_cache_dir(
        nint settings, string cache_dir);

    /// <summary>Largest text-encoder signature (tokens) to load; non-positive unsets.</summary>
    internal static void litert_lm_embedding_engine_settings_set_max_input_length(nint settings, int max_input_length)
    {
        int status = V1_litert_lm_embedding_engine_settings_set_max_input_length(settings, max_input_length);
        NativeError.Check(status, "litert_lm_embedding_engine_settings_set_max_input_length");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_settings_set_max_input_length")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_settings_set_max_input_length(
        nint settings, int max_input_length);

    /// <summary>Smallest text-encoder signature (tokens) to load; negative unsets.</summary>
    internal static void litert_lm_embedding_engine_settings_set_min_input_length(nint settings, int min_input_length)
    {
        int status = V1_litert_lm_embedding_engine_settings_set_min_input_length(settings, min_input_length);
        NativeError.Check(status, "litert_lm_embedding_engine_settings_set_min_input_length");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_settings_set_min_input_length")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_settings_set_min_input_length(
        nint settings, int min_input_length);

    /// <summary>Activation precision (0=F32, 1=F16, 2=I16, 3=I8, the <c>LiteRtLmActivationDataType</c> values).</summary>
    internal static void litert_lm_embedding_engine_settings_set_activation_data_type(nint settings, int activation_data_type)
    {
        int status = V1_litert_lm_embedding_engine_settings_set_activation_data_type(settings, activation_data_type);
        NativeError.Check(status, "litert_lm_embedding_engine_settings_set_activation_data_type");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_settings_set_activation_data_type")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_settings_set_activation_data_type(
        nint settings, int activation_data_type);

    // --- Embedding engine ------------------------------------------------

    internal static nint litert_lm_embedding_engine_create(nint settings)
    {
        nint result = default;
        int status = V1_litert_lm_embedding_engine_create(settings, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_create(
        nint settings, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_embedding_engine_delete(nint engine);

    /// <summary>Embeds one request made of <paramref name="num_inputs"/> input items (one text item for a
    /// text embedding). Null options = defaults. Returns a caller-owned response, null on failure.</summary>
    internal static nint litert_lm_embedding_engine_compute_embedding(nint engine, nint* inputs, nuint num_inputs, nint options)
    {
        nint result = default;
        int status = V1_litert_lm_embedding_engine_compute_embedding(engine, inputs, num_inputs, options, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_compute_embedding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_compute_embedding(
        nint engine, nint* inputs, nuint num_inputs, nint options, nint* out_result);

    /// <summary>Embeds <paramref name="batch_size"/> requests: <paramref name="inputs_batch"/>[i] points at
    /// request i's <paramref name="num_inputs_per_batch"/>[i] input items. Returns a caller-owned response
    /// collection, null on failure.</summary>
    internal static nint litert_lm_embedding_engine_compute_embedding_batch(nint engine, nint** inputs_batch, nuint* num_inputs_per_batch, nuint batch_size, nint options)
    {
        nint result = default;
        int status = V1_litert_lm_embedding_engine_compute_embedding_batch(engine, inputs_batch, num_inputs_per_batch, batch_size, options, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_engine_compute_embedding_batch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_engine_compute_embedding_batch(
        nint engine, nint** inputs_batch, nuint* num_inputs_per_batch, nuint batch_size, nint options, nint* out_result);

    // --- Embedding options -----------------------------------------------

    /// <summary>Options with the native defaults: normalize = true, insert special tokens = true, default
    /// output size and overflow strategy.</summary>
    internal static nint litert_lm_embedding_options_create()
    {
        nint result = default;
        int status = V1_litert_lm_embedding_options_create(&result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_options_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_options_create(
        nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_embedding_options_delete(nint options);

    internal static void litert_lm_embedding_options_set_normalize(nint options, bool normalize)
    {
        int status = V1_litert_lm_embedding_options_set_normalize(options, normalize);
        NativeError.Check(status, "litert_lm_embedding_options_set_normalize");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_options_set_normalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_options_set_normalize(
        nint options, [MarshalAs(UnmanagedType.U1)] bool normalize);

    internal static void litert_lm_embedding_options_set_insert_special_tokens(nint options, bool insert_special_tokens)
    {
        int status = V1_litert_lm_embedding_options_set_insert_special_tokens(options, insert_special_tokens);
        NativeError.Check(status, "litert_lm_embedding_options_set_insert_special_tokens");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_options_set_insert_special_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_options_set_insert_special_tokens(
        nint options, [MarshalAs(UnmanagedType.U1)] bool insert_special_tokens);

    internal static void litert_lm_embedding_options_set_input_overflow_strategy(nint options, int strategy)
    {
        int status = V1_litert_lm_embedding_options_set_input_overflow_strategy(options, strategy);
        NativeError.Check(status, "litert_lm_embedding_options_set_input_overflow_strategy");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_options_set_input_overflow_strategy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_options_set_input_overflow_strategy(
        nint options, int strategy);

    /// <summary>Truncates the output vector to this size (Matryoshka); 0 or negative restores the default.</summary>
    internal static void litert_lm_embedding_options_set_output_size(nint options, int output_size)
    {
        int status = V1_litert_lm_embedding_options_set_output_size(options, output_size);
        NativeError.Check(status, "litert_lm_embedding_options_set_output_size");
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_options_set_output_size")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_options_set_output_size(
        nint options, int output_size);

    // --- Embedding responses ---------------------------------------------

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_embedding_response_delete(nint response);

    internal static nuint litert_lm_embedding_response_get_size(nint response)
    {
        nuint result = default;
        int status = V1_litert_lm_embedding_response_get_size(response, &result);
        NativeError.Check(status, "litert_lm_embedding_response_get_size");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_response_get_size")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_response_get_size(
        nint response, nuint* out_result);

    /// <summary>The vector's values, owned by the response (valid while it lives); null when empty.</summary>
    internal static float* litert_lm_embedding_response_get_values(nint response)
    {
        float* result = default;
        int status = V1_litert_lm_embedding_response_get_values(response, &result);
        NativeError.Check(status, "litert_lm_embedding_response_get_values");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_response_get_values")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_response_get_values(
        nint response, float** out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_embedding_responses_delete(nint responses);

    internal static nuint litert_lm_embedding_responses_get_size(nint responses)
    {
        nuint result = default;
        int status = V1_litert_lm_embedding_responses_get_size(responses, &result);
        NativeError.Check(status, "litert_lm_embedding_responses_get_size");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_responses_get_size")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_responses_get_size(
        nint responses, nuint* out_result);

    /// <summary>The response at <paramref name="index"/>, owned by the collection (do not delete it); null
    /// when out of bounds.</summary>
    internal static nint litert_lm_embedding_responses_get_at(nint responses, nuint index)
    {
        nint result = default;
        int status = V1_litert_lm_embedding_responses_get_at(responses, index, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_embedding_responses_get_at")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_embedding_responses_get_at(
        nint responses, nuint index, nint* out_result);
}

/// <summary>Mirrors <c>LiteRtLmInputDataType</c> in engine.h.</summary>
internal enum LiteRtLmInputDataType
{
    Text = 0,
    Image = 1,
    ImageEnd = 2,
    Audio = 3,
    AudioEnd = 4,
}
