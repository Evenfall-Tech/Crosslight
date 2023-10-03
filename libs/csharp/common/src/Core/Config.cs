using Crosslight.Core.Utilities;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Crosslight.Core;

/// <summary>
/// Key-value pair configuration wrapper.
/// Hierarchial keys supported through <c>key1/key2</c>.
/// </summary>
public class Config : IDisposable
{
    private class ConfigImported : IImported
    {
        [DllImport("cl_core", EntryPoint = "cl_config_init")]
        public static extern nint ConfigInit();

        [DllImport("cl_core", EntryPoint = "cl_config_term")]
        public static extern nuint ConfigTerm(
            nint config);

        [DllImport("cl_core", EntryPoint = "cl_config_string_get")]
        public static extern nint ConfigStringGet(
            nint context,
            nint key);

        [DllImport("cl_core", EntryPoint = "cl_config_string_set")]
        public static extern nuint ConfigStringSet(
            nint context,
            nint key,
            nint value);
    }

    private nint _context;
    private bool _disposed;
    private bool _shouldDispose;

    /// <summary>
    /// Get a string value from the config based on a key.
    /// </summary>
    /// <param name="key">The key to get the value for.</param>
    /// <returns><see langword="null"/> if no such key found or value is <see langword="null"/>, the string otherwise.</returns>
    /// <remarks>The returned string, if any, is a copy of the value inside the config.</remarks>
    public string? GetString(string key)
    {
        CheckDisposed();

        using var keyStr = new Utf8String(key);
        var result = ConfigImported.ConfigStringGet(_context, keyStr.Context);

        if (result == 0)
        {
            return null;
        }

        return Marshal.PtrToStringUTF8(result);
    }

    /// <summary>
    /// Set a string value in the config based on a key.
    /// If the key is already present, replace the value.
    /// </summary>
    /// <param name="key">The key to set the value for.</param>
    /// <param name="value">The value to set. Will be copied. Can be <see langword="null"/>.</param>
    /// <returns><see langword="false"/> if setting value for key failed, <see langword="true"/> otherwise.</returns>
    /// <remarks>If a memory allocation error occurs, the function should terminate gracefully and not change the <see cref="Config"/> state.</remarks>
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

        return result == 1;
    }

    /// <summary>
    /// Create a new instance of the config.
    /// </summary>
    /// <remarks>The caller is responsible for deleting the instance by disposing.</remarks>
    public Config()
    {
        var result = ConfigImported.ConfigInit();

        _context = result;
        _shouldDispose = true;
    }

    /// <summary>
    /// Bind an existing instance of the config.
    /// </summary>
    /// <param name="context">The existing instance pointer.</param>
    /// <remarks>The caller is responsible for deleting the instance using the pointer.</remarks>
    public Config(nint context)
    {
        _context = context;
        _shouldDispose = false;
    }

    /// <summary>
    /// Delete an instance of the config.
    /// </summary>
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
        if (_shouldDispose)
        {
            _ = ConfigImported.ConfigTerm(_context);
        }
    }
}