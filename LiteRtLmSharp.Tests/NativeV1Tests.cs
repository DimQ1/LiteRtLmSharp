using LiteRtLmSharp.Native;
using Xunit;

namespace LiteRtLmSharp.Tests;

public sealed class NativeV1Tests
{
    [Fact]
    public void Setter_ShouldSurfaceNativeStatus_InsteadOfIgnoringFailure()
    {
        LiteRtEngine.SetMinLogLevel(3);

        var exception = Assert.Throws<LiteRtException>(() =>
            LiteRtLmNative.litert_lm_engine_settings_set_max_num_tokens(nint.Zero, 1024));

        Assert.Equal(LiteRtStatusCode.InvalidArgument, exception.StatusCode);
        Assert.Contains("litert_lm_engine_settings_set_max_num_tokens", exception.Message);
        LiteRtEngine.SetMinLogLevel(3);
    }

    [Fact]
    public void Metadata_ShouldRejectInvalidHandle_InsteadOfReportingMissingMetadata()
    {
        LiteRtEngine.SetMinLogLevel(3);

        var exception = Assert.Throws<LiteRtException>(() =>
            LiteRtLmNative.litert_lm_loaded_file_embedding_dimension(nint.Zero));

        Assert.Equal(LiteRtStatusCode.InvalidArgument, exception.StatusCode);
    }

    [Fact]
    public void InvalidLogLevel_ShouldPreserveStatus_WhenNativeMessageIsEmpty()
    {
        LiteRtEngine.SetMinLogLevel(1000);
        try
        {
            var exception = Assert.Throws<LiteRtException>(() => LiteRtEngine.SetMinLogLevel(-123));
            Assert.Equal(LiteRtStatusCode.InvalidArgument, exception.StatusCode);
        }
        finally
        {
            LiteRtEngine.SetMinLogLevel(3);
        }
    }
}
