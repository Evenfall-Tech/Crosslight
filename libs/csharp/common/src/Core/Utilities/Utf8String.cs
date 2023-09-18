using System.Runtime.InteropServices;
using System.Text;

namespace Crosslight.Core.Utilities;

internal class Utf8String : IDisposable
{
    private nint _context;
    private int _bufferSize;

    public nint Context => _context;
    public int BufferLength => _bufferSize;

    public Utf8String(string value)
    {
        if (value == null)
        {
            _context = 0;
        }
        else
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);

            _context = Marshal.AllocHGlobal(bytes.Length + 1);
            Marshal.Copy(bytes, 0, _context, bytes.Length);
            Marshal.WriteByte(_context, bytes.Length, 0);

            _bufferSize = bytes.Length + 1;
        }
    }

    public void Dispose()
    {
        if (_context != 0)
        {
            Marshal.FreeHGlobal(_context);
            _context = 0;
        }
    }
}
