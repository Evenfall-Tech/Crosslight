using Crosslight.Core.Utilities;
using System.Runtime.InteropServices;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Core.Nodes;

/// <summary>
/// Access modifier type. Access modifiers do not support type combinations.
/// </summary>
public enum AccessModifierType : uint
{
    /// <summary>
    /// No access modifier. Acts as a fallback, should not be used normally.
    /// </summary>
    None = 0,
    /// <summary>
    /// The type or member can be accessed by any other code in the current or other compilation units.
    /// </summary>
    Public = 1,
    /// <summary>
    /// The type or member can be accessed only by sibling members or derivatives of the parent type.
    /// </summary>
    Protected = 2,
    /// <summary>
    /// The type or member can be accessed only by sibling members.
    /// </summary>
    Private = 3,
}

/// <summary>
/// Access modifier for types, members, and other nodes.
/// </summary>
public struct AccessModifier : INodePayload
{
    private struct Imported
    {
        public nuint Type;
    }

    /// <summary>
    /// Type of the access modifier. Does not support type combinations.
    /// </summary>
    public AccessModifierType Kind;

    /// <summary>
    /// Create a new managed instance of the access modifier payload from the native representation.
    /// </summary>
    /// <param name="pointer">The native pointer to an access modifier payload.</param>
    public AccessModifier(nint pointer)
    {
        Imported imported = Marshal.PtrToStructure<Imported>(pointer);

        Kind = (AccessModifierType)(uint)imported.Type;
    }

    /// <inheritdoc/>
    public readonly NodeType Type => NodeType.AccessModifier;

    /// <inheritdoc/>
    public readonly nint ToPointer(AcquireDelegate? acquire)
    {
        acquire ??= Marshal.AllocCoTaskMem;
        nint pointer = acquire(Marshal.SizeOf<Imported>());

        if (pointer == 0)
        {
            return 0;
        }

        Imported imported = new()
        {
            Type = (nuint)this.Kind,
        };

        Marshal.StructureToPtr(imported, pointer, false);

        return pointer;
    }

    public readonly object? AcceptVisitor(object context, Node node, INodePayloadVisitor visitor)
    {
        return visitor.VisitAccessModifier(context, node, this);
    }

    public override string ToString()
    {
        return $"{{ {nameof(AccessModifier)}-{Type} }}";
    }
}
