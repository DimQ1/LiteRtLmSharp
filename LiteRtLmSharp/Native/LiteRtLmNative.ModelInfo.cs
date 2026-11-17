using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LiteRtLmSharp.Native;

// Model metadata read from a .litertlm file without loading an engine (c/model_info.h, native v0.18.0).
internal static unsafe partial class LiteRtLmNative
{
    /// <summary>Opens a model file for capability queries; null when the file cannot be opened. The path is
    /// null-terminated. Use <see cref="NarrowPath.Encode"/>: the Windows loader needs an ASCII alias
    /// for non-ASCII paths because its file-inspection and stream-reading layers use different encodings.</summary>
    internal static nint litert_lm_loaded_file_create(byte* litertlm_path)
    {
        nint result = default;
        int status = V1_litert_lm_loaded_file_create(litertlm_path, &result);
        NativeError.Record(status);
        return status == 0 ? result : nint.Zero;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_create(
        byte* litertlm_path, nint* out_result);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void litert_lm_loaded_file_delete(nint loaded_file);

    internal static LiteRtLmModelType litert_lm_loaded_file_model_type(nint loaded_file)
    {
        LiteRtLmModelType result = default;
        int status = V1_litert_lm_loaded_file_model_type(loaded_file, &result);
        NativeError.Check(status, "litert_lm_loaded_file_model_type");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_model_type")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_model_type(
        nint loaded_file, LiteRtLmModelType* out_result);

    internal static bool litert_lm_loaded_file_has_speculative_decoding_support(nint loaded_file)
    {
        byte result = default;
        int status = V1_litert_lm_loaded_file_has_speculative_decoding_support(loaded_file, &result);
        NativeError.Check(status, "litert_lm_loaded_file_has_speculative_decoding_support");
        return result != 0;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_has_speculative_decoding_support")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_has_speculative_decoding_support(
        nint loaded_file, byte* out_result);

    /// <summary>False when the metadata does not declare it.</summary>
    internal static bool litert_lm_loaded_file_supports_thinking(nint loaded_file)
    {
        byte result = default;
        int status = V1_litert_lm_loaded_file_supports_thinking(loaded_file, &result);
        NativeError.Check(status, "litert_lm_loaded_file_supports_thinking");
        return result != 0;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_supports_thinking")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_supports_thinking(
        nint loaded_file, byte* out_result);

    /// <summary>False when the metadata does not declare it.</summary>
    internal static bool litert_lm_loaded_file_supports_function_calling(nint loaded_file)
    {
        byte result = default;
        int status = V1_litert_lm_loaded_file_supports_function_calling(loaded_file, &result);
        NativeError.Check(status, "litert_lm_loaded_file_supports_function_calling");
        return result != 0;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_supports_function_calling")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_supports_function_calling(
        nint loaded_file, byte* out_result);

    internal static LiteRtLmSamplerType litert_lm_loaded_file_sampler_type(nint loaded_file)
    {
        LiteRtLmSamplerType result = default;
        int status = V1_litert_lm_loaded_file_sampler_type(loaded_file, &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return LiteRtLmSamplerType.Unspecified;
        }
        NativeError.Check(status, "litert_lm_loaded_file_sampler_type");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_sampler_type")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_sampler_type(
        nint loaded_file, LiteRtLmSamplerType* out_result);

    internal static float litert_lm_loaded_file_sampler_temperature(nint loaded_file)
    {
        float result = default;
        int status = V1_litert_lm_loaded_file_sampler_temperature(loaded_file, &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return 0;
        }
        NativeError.Check(status, "litert_lm_loaded_file_sampler_temperature");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_sampler_temperature")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_sampler_temperature(
        nint loaded_file, float* out_result);

    internal static int litert_lm_loaded_file_sampler_top_k(nint loaded_file)
    {
        int result = default;
        int status = V1_litert_lm_loaded_file_sampler_top_k(loaded_file, &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return 0;
        }
        NativeError.Check(status, "litert_lm_loaded_file_sampler_top_k");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_sampler_top_k")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_sampler_top_k(
        nint loaded_file, int* out_result);

    internal static float litert_lm_loaded_file_sampler_top_p(nint loaded_file)
    {
        float result = default;
        int status = V1_litert_lm_loaded_file_sampler_top_p(loaded_file, &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return 0;
        }
        NativeError.Check(status, "litert_lm_loaded_file_sampler_top_p");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_sampler_top_p")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_sampler_top_p(
        nint loaded_file, float* out_result);

    internal static bool litert_lm_loaded_file_supports_input_modality(nint loaded_file, LiteRtLmModality modality)
    {
        byte result = default;
        int status = V1_litert_lm_loaded_file_supports_input_modality(loaded_file, modality, &result);
        NativeError.Check(status, "litert_lm_loaded_file_supports_input_modality");
        return result != 0;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_supports_input_modality")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_supports_input_modality(
        nint loaded_file, LiteRtLmModality modality, byte* out_result);

    /// <summary>-1 when the model has no vision or declares no budget.</summary>
    internal static int litert_lm_loaded_file_max_vision_token_budget(nint loaded_file)
    {
        int result = default;
        int status = V1_litert_lm_loaded_file_max_vision_token_budget(loaded_file, &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return -1;
        }
        NativeError.Check(status, "litert_lm_loaded_file_max_vision_token_budget");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_max_vision_token_budget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_max_vision_token_budget(
        nint loaded_file, int* out_result);

    /// <summary>Fixed context size of a static model, or the largest settable size of a dynamic one; 0 when
    /// not found.</summary>
    internal static uint litert_lm_loaded_file_max_context_tokens(nint loaded_file)
    {
        uint result = default;
        int status = V1_litert_lm_loaded_file_max_context_tokens(loaded_file, &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return 0;
        }
        NativeError.Check(status, "litert_lm_loaded_file_max_context_tokens");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_max_context_tokens")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_max_context_tokens(
        nint loaded_file, uint* out_result);

    internal static bool litert_lm_loaded_file_is_dynamic_context(nint loaded_file)
    {
        byte result = default;
        int status = V1_litert_lm_loaded_file_is_dynamic_context(loaded_file, &result);
        NativeError.Check(status, "litert_lm_loaded_file_is_dynamic_context");
        return result != 0;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_is_dynamic_context")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_is_dynamic_context(
        nint loaded_file, byte* out_result);

    /// <summary>Writes up to <paramref name="max_size"/> vision token lengths and returns their count (null
    /// <paramref name="lengths"/> returns the count only); -1 when the model has no vision.</summary>
    internal static int litert_lm_loaded_file_vision_signature_selection(nint loaded_file, int* lengths, int max_size)
    {
        nuint result = default;
        int status = V1_litert_lm_loaded_file_vision_signature_selection(loaded_file, lengths, checked((nuint)max_size), &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return -1;
        }
        NativeError.Check(status, "litert_lm_loaded_file_vision_signature_selection");
        return checked((int)result);
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_vision_signature_selection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_vision_signature_selection(
        nint loaded_file, int* lengths, nuint max_size, nuint* out_result);

    /// <summary>Writes up to <paramref name="max_size"/> backends for a modality in priority order and returns
    /// their count (null <paramref name="backends"/> returns the count only); 0 when unsupported.</summary>
    internal static int litert_lm_loaded_file_modality_supported_backends(nint loaded_file, LiteRtLmModality modality, LiteRtLmBackendType* backends, int max_size)
    {
        nuint result = default;
        int status = V1_litert_lm_loaded_file_modality_supported_backends(loaded_file, modality, backends, checked((nuint)max_size), &result);
        NativeError.Check(status, "litert_lm_loaded_file_modality_supported_backends");
        return checked((int)result);
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_modality_supported_backends")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_modality_supported_backends(
        nint loaded_file, LiteRtLmModality modality, LiteRtLmBackendType* backends, nuint max_size, nuint* out_result);

    /// <summary>Minimum runtime version string owned by the loaded file; null when undefined.</summary>
    internal static nint litert_lm_loaded_file_min_runtime_version(nint loaded_file)
    {
        nint result = default;
        int status = V1_litert_lm_loaded_file_min_runtime_version(loaded_file, &result);
        NativeError.Check(status, "litert_lm_loaded_file_min_runtime_version");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_min_runtime_version")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_min_runtime_version(
        nint loaded_file, nint* out_result);

    /// <summary>-1 when the model is not an embedding model or declares no dimension.</summary>
    internal static int litert_lm_loaded_file_embedding_dimension(nint loaded_file)
    {
        int result = default;
        int status = V1_litert_lm_loaded_file_embedding_dimension(loaded_file, &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return -1;
        }
        NativeError.Check(status, "litert_lm_loaded_file_embedding_dimension");
        return result;
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_embedding_dimension")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_embedding_dimension(
        nint loaded_file, int* out_result);

    /// <summary>Writes up to <paramref name="max_size"/> embedding input lengths and returns their count
    /// (null <paramref name="lengths"/> returns the count only); -1 when not an embedding model.</summary>
    internal static int litert_lm_loaded_file_embedding_signature_selection(nint loaded_file, int* lengths, int max_size)
    {
        nuint result = default;
        int status = V1_litert_lm_loaded_file_embedding_signature_selection(loaded_file, lengths, checked((nuint)max_size), &result);
        if (status == (int)LiteRtStatusCode.NotFound)
        {
            NativeError.Clear();
            return -1;
        }
        NativeError.Check(status, "litert_lm_loaded_file_embedding_signature_selection");
        return checked((int)result);
    }

    [LibraryImport(Library, EntryPoint = "litert_lm_loaded_file_embedding_signature_selection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int V1_litert_lm_loaded_file_embedding_signature_selection(
        nint loaded_file, int* lengths, nuint max_size, nuint* out_result);
}

/// <summary>Mirrors <c>LiteRtLmModelType</c> in model_info.h.</summary>
internal enum LiteRtLmModelType
{
    Unknown = 0,
    Llm = 1,
    Embedding = 2,
}

/// <summary>Mirrors <c>LiteRtLmModality</c> in model_info.h.</summary>
internal enum LiteRtLmModality
{
    Text = 0,
    Vision = 1,
    Audio = 2,
    Video = 3,
}

/// <summary>Mirrors <c>LiteRtLmBackendType</c> in model_info.h.</summary>
internal enum LiteRtLmBackendType
{
    Cpu = 1,
    Gpu = 2,
    Npu = 3,
}
