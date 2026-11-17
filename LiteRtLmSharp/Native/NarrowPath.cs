using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace LiteRtLmSharp.Native;

internal static partial class NarrowPath
{
    internal static byte[] Encode(string path)
    {
        if (!OperatingSystem.IsWindows() || IsAscii(path))
            return Encoding.UTF8.GetBytes(path + '\0');

        // The 0.19.1 metadata loader mixes UTF-8 file inspection with a narrow ifstream read.
        // ANSI fixes one layer and breaks the other; an ASCII short alias is valid for both.
        string alias = ShortPath(Path.GetFullPath(path));
        if (!string.IsNullOrEmpty(alias) && IsAscii(alias))
            return Encoding.UTF8.GetBytes(alias + '\0');

        throw new LiteRtException(
            "The native metadata reader cannot open this non-ASCII Windows path and the volume has no " +
            "ASCII short-name alias. Choose a model storage folder with an ASCII path.");
    }

    private static bool IsAscii(string path) => path.All(c => c <= 127);

    [SupportedOSPlatform("windows")]
    private static unsafe string ShortPath(string path)
    {
        uint length = GetShortPathNameW(path, null, 0);
        if (length == 0)
            return "";
        var buffer = new char[checked((int)length)];
        fixed (char* pointer = buffer)
        {
            uint written = GetShortPathNameW(path, pointer, length);
            return written > 0 && written < length ? new string(pointer, 0, checked((int)written)) : "";
        }
    }

    [LibraryImport("kernel32.dll", EntryPoint = "GetShortPathNameW", StringMarshalling = StringMarshalling.Utf16)]
    [SupportedOSPlatform("windows")]
    private static unsafe partial uint GetShortPathNameW(string path, char* result, uint length);
}
