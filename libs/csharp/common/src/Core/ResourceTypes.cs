using Crosslight.Core.Utilities;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;

namespace Crosslight.Core;

/// <summary>
/// List of supported MIME-types for a certain language.
/// </summary>
public class ResourceTypes
{
    [StructLayout(LayoutKind.Sequential)]
    private struct ResourceTypesImported : IImported
    {
        public nint ContentTypes;
        public nuint ContentTypesSize;
    }

    /// <summary>
    /// MIME-type array.
    /// </summary>
    public IEnumerable<string> ContentTypes { get; }

    public ResourceTypes(nint resourceTypesPtr)
    {
        ResourceTypesImported resourceTypes = Marshal.PtrToStructure<ResourceTypesImported>(resourceTypesPtr);

        if (resourceTypes.ContentTypes == 0)
        {
            ContentTypes = Array.Empty<string>();
        }
        else
        {
            var contentTypes = new List<string>();
            var size = (int)resourceTypes.ContentTypesSize;
            var offset = Marshal.SizeOf<nint>();

            for (int i = 0; i < size; ++i)
            {
                var typePtr = Marshal.ReadIntPtr(resourceTypes.ContentTypes, i * offset);
                var type = Marshal.PtrToStringUTF8(typePtr);

                if (type != null)
                {
                    contentTypes.Add(type);
                }
            }

            ContentTypes = contentTypes;
        }
    }

    public ResourceTypes(IEnumerable<string> contentTypes)
    {
        ContentTypes = contentTypes.ToArray(); // Copy just in case.
    }

    public nint ToPointer()
    {
        nint pointer = Marshal.AllocHGlobal(Marshal.SizeOf<ResourceTypesImported>());
        nint contentTypes = 0;
        int contentTypesSize = ContentTypes.Count();

        if (contentTypesSize > 0)
        {
            var offset = Marshal.SizeOf<nint>();
            contentTypes = Marshal.AllocHGlobal(offset * contentTypesSize);

            int i = 0;
            foreach (var type in ContentTypes)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(type);
                var contentTypePtr = Marshal.AllocHGlobal(bytes.Length + 1);
                Marshal.Copy(bytes, 0, contentTypePtr, bytes.Length);
                Marshal.WriteByte(contentTypePtr, bytes.Length, 0);
                Marshal.WriteIntPtr(contentTypes + i * offset, contentTypePtr);
                ++i;
            }
        }

        ResourceTypesImported resourceTypes = new ResourceTypesImported()
        {
            ContentTypesSize = (nuint)contentTypesSize,
            ContentTypes = contentTypes,
        };

        Marshal.StructureToPtr(resourceTypes, pointer, false);

        return pointer;
    }

    public static bool TermPointer(nint resourceTypesPtr)
    {
        ResourceTypesImported resourceTypes = Marshal.PtrToStructure<ResourceTypesImported>(resourceTypesPtr);

        if (resourceTypes.ContentTypes != 0 && resourceTypes.ContentTypesSize != 0)
        {
            var size = (int)resourceTypes.ContentTypesSize;
            var offset = Marshal.SizeOf<nint>();

            for (int i = 0; i < size; ++i)
            {
                var typePtr = Marshal.ReadIntPtr(resourceTypes.ContentTypes, i * offset);
                Marshal.FreeHGlobal(typePtr);
            }

            Marshal.FreeHGlobal(resourceTypes.ContentTypes);
        }

        Marshal.FreeHGlobal(resourceTypesPtr);
        return true;
    }
}
