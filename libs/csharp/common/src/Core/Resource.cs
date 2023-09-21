using Crosslight.Core.Utilities;
using System.Runtime.InteropServices;
using System.Text;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Core;

/// <summary>
/// External data resource. Usually in form of a text source file, but can be any arbitrary data.
/// </summary>
public class Resource
{
    [StructLayout(LayoutKind.Sequential)]
    private struct ResourceImported
    {
        public nint Content;
        public nuint ContentSize;
        public nint ContentType;
    }

    private byte[]? _content;
    private string? _contentType;

    /// <summary>
    /// Data, provided by the resource.
    /// </summary>
    public byte[]? Content => _content;

    /// <summary>
    /// MIME-type of the data.
    /// </summary>
    public string? ContentType => _contentType;

    /// <summary>
    /// Create a new resource that holds the content.
    /// </summary>
    /// <param name="content">Data, provided by the resource.</param>
    /// <param name="contentType">MIME-type of the data.</param>
    public Resource(byte[]? content, string? contentType)
    {
        _content = content;
        _contentType = contentType;
    }

    /// <summary>
    /// Create a new managed copy of the resource from a native pointer.
    /// </summary>
    /// <param name="resourcePtr">Native pointer to the previously created resource.</param>
    public Resource(nint resourcePtr)
    {
        ResourceImported resource = Marshal.PtrToStructure<ResourceImported>(resourcePtr);

        _contentType = resource.ContentType == 0
            ? null
            : Marshal.PtrToStringUTF8(resource.ContentType);

        if (resource.Content == 0)
        {
            _content = null;
        }
        else
        {
            _content = new byte[resource.ContentSize];
            Marshal.Copy(resource.Content, _content, 0, _content.Length);
        }
    }

    /// <summary>
    /// Convert this resource to a native pointer.
    /// </summary>
    /// <param name="acquire">Delegate to allocate memory for the resource.</param>
    /// <returns>The native pointer, leading to the allocated native representation of the resource.</returns>
    public nint ToPointer(AcquireDelegate? acquire = null)
    {
        acquire ??= Marshal.AllocCoTaskMem;
        nint pointer = acquire(Marshal.SizeOf<ResourceImported>());

        nint contentPtr = 0;

        if (_content != null)
        {
            contentPtr = acquire(_content.Length);
            Marshal.Copy(_content, 0, contentPtr, _content.Length);
        }

        nint contentTypePtr = 0;

        if (_contentType != null)
        {
            contentTypePtr = Utf8String.ToPointer(_contentType, acquire);
        }

        ResourceImported resource = new()
        {
            ContentSize = (nuint)(_content?.Length ?? 0),
            Content = contentPtr,
            ContentType = contentTypePtr,
        };

        Marshal.StructureToPtr(resource, pointer, false);

        return pointer;
    }
}
