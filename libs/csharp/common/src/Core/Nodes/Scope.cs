using Crosslight.Core.Nodes;
using Crosslight.Core.Utilities;
using System.Runtime.InteropServices;
using Crosslight.Core;
using static Crosslight.Core.ILanguage;

namespace Crosslight.src.Core.Nodes
{
    /// <summary>
    /// Declaration scope, mapping to e.g. namespaces.
    /// </summary>
    /// <remarks>
    /// Empty scope is assumed to be global. If a truly empty scope is desired, an empty node can be placed inside.
    /// </remarks>
    public struct Scope : INodePayload
    {
        private struct Imported
        {
            public nint Identifier;
        }

        /// <summary>
        /// Full identifier of the scope, can be separated with '.'. Can be empty or <see langword="null"/>.
        /// </summary>
        public string? Identifier;

        /// <summary>
        /// Create a new managed instance of the scope payload from the native representation.
        /// </summary>
        /// <param name="pointer">The native pointer to a scope payload.</param>
        public Scope(nint pointer)
        {
            Imported imported = Marshal.PtrToStructure<Imported>(pointer);

            Identifier = imported.Identifier == 0
                ? null
                : Marshal.PtrToStringUTF8(imported.Identifier);
        }

        /// <inheritdoc/>
        public readonly NodeType Type => NodeType.Scope;

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
            return visitor.VisitScope(node, this);
        }

        public override string ToString()
        {
            return $"{{ {nameof(Scope)}-{Identifier} }}";
        }
    }
}
