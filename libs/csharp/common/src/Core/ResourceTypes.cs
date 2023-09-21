using Crosslight.Core.Utilities;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Core;

/// <summary>
/// List of supported MIME-types for a certain language.
/// </summary>
public class ResourceTypes
{
    [StructLayout(LayoutKind.Sequential)]
    private struct ResourceTypesImported
    {
        public nint ContentTypes;
        public nuint ContentTypesSize;
    }

    /// <summary>
    /// MIME-type array.
    /// </summary>
    public IEnumerable<string> ContentTypes { get; }

    /// <summary>
    /// Create a new managed copy of the resource types from a native pointer.
    /// </summary>
    /// <param name="resourcePtr">Native pointer to the previously created resource types.</param>
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

    /// <summary>
    /// Create a new list of supported content types.
    /// </summary>
    /// <param name="contentTypes">A set of supported content types to hold.</param>
    public ResourceTypes(IEnumerable<string> contentTypes)
    {
        ContentTypes = contentTypes.ToArray(); // Copy just in case.
    }

    /// <summary>
    /// Convert this resource types list to a native pointer.
    /// </summary>
    /// <param name="acquire">Delegate to allocate memory for the resource types.</param>
    /// <returns>The native pointer, leading to the allocated native representation of the resource types.</returns>
    public nint ToPointer(AcquireDelegate? acquire = null)
    {
        acquire ??= Marshal.AllocCoTaskMem;
        nint pointer = acquire(Marshal.SizeOf<ResourceTypesImported>());
        nint contentTypes = 0;
        int contentTypesSize = ContentTypes.Count();

        if (contentTypesSize > 0)
        {
            var offset = Marshal.SizeOf<nint>();
            contentTypes = acquire(offset * contentTypesSize);

            int i = 0;
            foreach (var type in ContentTypes)
            {
                var contentTypePtr = Utf8String.ToPointer(type, acquire);
                Marshal.WriteIntPtr(contentTypes + i * offset, contentTypePtr);
                ++i;
            }
        }

        ResourceTypesImported resourceTypes = new()
        {
            ContentTypesSize = (nuint)contentTypesSize,
            ContentTypes = contentTypes,
        };

        Marshal.StructureToPtr(resourceTypes, pointer, false);

        return pointer;
    }

    /// <summary>
    /// Delete the instance of the created resource types.
    /// </summary>
    /// <param name="resourceTypesPtr">The native pointer to resource types, previously acquired by <see cref="ToPointer(AcquireDelegate?)"/>.</param>
    /// <param name="release">The delegate to free memory for the resource types.</param>
    /// <returns><see langword="true"/> if termination succeeded, <see langword="false"/> otherwise.</returns>
    public static bool TermPointer(
        nint resourceTypesPtr,
        ReleaseDelegate? release = null)
    {
        if (resourceTypesPtr == 0)
        {
            return true;
        }

        release ??= Marshal.FreeCoTaskMem;
        ResourceTypesImported resourceTypes = Marshal.PtrToStructure<ResourceTypesImported>(resourceTypesPtr);

        if (resourceTypes.ContentTypes != 0 && resourceTypes.ContentTypesSize != 0)
        {
            var size = (int)resourceTypes.ContentTypesSize;
            var offset = Marshal.SizeOf<nint>();

            for (int i = 0; i < size; ++i)
            {
                var typePtr = Marshal.ReadIntPtr(resourceTypes.ContentTypes, i * offset);
                release(typePtr);
            }

            release(resourceTypes.ContentTypes);
        }

        release(resourceTypesPtr);
        return true;
    }
}
