using Crosslight.Core.Utilities;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Crosslight.Core;

public class Config : IDisposable
{
    private class ConfigImported : IImported
    {
        [DllImport("cl_core", EntryPoint = "cl_config_init")]
        public static extern nuint ConfigInit();

        [DllImport("cl_core", EntryPoint = "cl_config_term")]
        public static extern nuint ConfigTerm(
            nuint config);

        [DllImport("cl_core", EntryPoint = "cl_config_string_get")]
        public static extern nint ConfigStringGet(
            nuint context,
            nint key);

        [DllImport("cl_core", EntryPoint = "cl_config_string_set")]
        public static extern nuint ConfigStringSet(
            nuint context,
            nint key,
            nint value);
    }

    private nuint _context;
    private bool _disposed;

    public string? GetString(string key)
    {
        CheckDisposed();

        using var keyStr = new Utf8String(key);
        var result = ConfigImported.ConfigStringGet(_context, keyStr.Context);
        Console.WriteLine($"{_context} Get {key}: {result}");

        if (result == 0)
        {
            return null;
        }

        return Marshal.PtrToStringUTF8(result);
    }

    public bool SetString(string key, string? value)
    {
        CheckDisposed();

        nuint result;
        using var keyStr = new Utf8String(key);

        if (value == null)
        {
            result = ConfigImported.ConfigStringSet(_context, keyStr.Context, 0);
        }
        else
        {
            using var valueStr = new Utf8String(value);
            result = ConfigImported.ConfigStringSet(_context, keyStr.Context, valueStr.Context);
        }

        Console.WriteLine($"{_context} Set {key}: {result}");
        return result == 1;
    }

    public Config()
    {
        var result = ConfigImported.ConfigInit();

        _context = result;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        InternalDispose();
        GC.SuppressFinalize(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(Config));
        }
    }

    ~Config()
    {
        InternalDispose();
    }

    private void InternalDispose()
    {
        var result = ConfigImported.ConfigTerm(_context);
    }
}