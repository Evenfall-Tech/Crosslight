using Crosslight.Core.Utilities;
using System.Runtime.InteropServices;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Core.Nodes;

/// <summary>
/// Complex type stored on the heap, mapping to e.g. allocated classes.
/// </summary>
public struct HeapType : INodePayload
{
    private struct Imported
    {
        public nint Identifier;
    }

    /// <summary>
    /// Identifier of the type. Can be empty or <see langword="null"/>.
    /// </summary>
    public string? Identifier;

    /// <summary>
    /// Create a new managed instance of the heap type payload from the native representation.
    /// </summary>
    /// <param name="pointer">The native pointer to a heap type payload.</param>
    public HeapType(nint pointer)
    {
        Imported imported = Marshal.PtrToStructure<Imported>(pointer);

        Identifier = imported.Identifier == 0
            ? null
            : Marshal.PtrToStringUTF8(imported.Identifier);
    }

    /// <inheritdoc/>
    public readonly NodeType Type => NodeType.HeapType;

    /// <inheritdoc/>
    public readonly nint ToPointer(AcquireDelegate? acquire)
    {
        acquire ??= Marshal.AllocCoTaskMem;
        nint pointer = acquire(Marshal.SizeOf<Imported>());

        if (pointer == 0)
        {
            return 0;
        }

        nint identifier = Identifier == null
            ? 0
            : Utf8String.ToPointer(Identifier, acquire);

        Imported imported = new()
        {
            Identifier = identifier,
        };

        Marshal.StructureToPtr(imported, pointer, false);

        return pointer;
    }

    public readonly object? AcceptVisitor(Node node, INodePayloadVisitor visitor)
    {
        return visitor.VisitHeapType(node, this);
    }

    public override string ToString()
    {
        return $"{{ {nameof(HeapType)}-{Identifier} }}";
    }
}