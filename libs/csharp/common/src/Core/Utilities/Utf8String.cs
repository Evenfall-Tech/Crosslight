using System.Runtime.InteropServices;
using System.Text;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Core.Utilities;

internal class Utf8String : IDisposable
{
    private nint _context;
    private int _bufferSize;
    private AcquireDelegate _acquire;
    private ReleaseDelegate _release;

    public nint Context => _context;
    public int BufferLength => _bufferSize;

    public Utf8String(string value, AcquireDelegate? acquire = null, ReleaseDelegate? release = null)
    {
        _acquire = acquire ?? Marshal.AllocCoTaskMem;
        _release = release ?? Marshal.FreeCoTaskMem;

        if (value == null)
        {
            _context = 0;
        }
        else
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            _context = _acquire(bytes.Length + 1);

            if (_context != 0)
            {
                Marshal.Copy(bytes, 0, _context, bytes.Length);
                Marshal.WriteByte(_context, bytes.Length, 0);

                _bufferSize = bytes.Length + 1;
            }
            else
            {
                _bufferSize = 0;
            }
        }
    }

    public void Dispose()
    {
        if (_context != 0)
        {
            _release(_context);
            _context = 0;
        }
    }

    /// <summary>
    /// Convert a UTF-8 string to an unmanaged native pointer representation.
    /// </summary>
    /// <param name="text">Text to convert.</param>
    /// <param name="acquire">Delegate to allocate memory for the string.</param>
    /// <returns>A native pointer to the allocated string or <c>0</c> on error.</returns>
    public static nint ToPointer(string text, AcquireDelegate? acquire = null)
    {
        acquire ??= Marshal.AllocCoTaskMem;

        byte[] bytes = Encoding.UTF8.GetBytes(text);
        var ptr = acquire(bytes.Length + 1);

        if (ptr != 0)
        {
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            Marshal.WriteByte(ptr, bytes.Length, 0);
        }

        return ptr;
    }
}
