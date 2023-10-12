using Crosslight.Core.Nodes;
using Crosslight.Core.Utilities;
using System.Runtime.InteropServices;
using Crosslight.Core;
using static Crosslight.Core.ILanguage;

namespace Crosslight.src.Core.Nodes
{
    /// <summary>
    /// Textual source file root.
    /// </summary>
    public struct SourceRoot : INodePayload
    {
        private struct Imported
        {
            public nint FileName;
        }

        /// <summary>
        /// Name of the file that contains this root in UTF-8 format. Can be empty or <see langword="null"/>.
        /// </summary>
        public string? FileName;

        /// <summary>
        /// Create a new managed instance of the source root payload from the native representation.
        /// </summary>
        /// <param name="pointer">The native pointer to a source root payload.</param>
        public SourceRoot(nint pointer)
        {
            Imported imported = Marshal.PtrToStructure<Imported>(pointer);

            FileName = imported.FileName == 0
                ? null
                : Marshal.PtrToStringUTF8(imported.FileName);
        }

        /// <inheritdoc/>
        public readonly NodeType Type => NodeType.SourceRoot;

        /// <inheritdoc/>
        public readonly nint ToPointer(AcquireDelegate? acquire)
        {
            acquire ??= Marshal.AllocCoTaskMem;
            nint pointer = acquire(Marshal.SizeOf<Imported>());

            if (pointer == 0)
            {
                return 0;
            }

            nint fileName = FileName == null
                ? 0
                : Utf8String.ToPointer(FileName, acquire);

            Imported imported = new()
            {
                FileName = fileName,
            };

            Marshal.StructureToPtr(imported, pointer, false);

            return pointer;
        }

        public readonly object? AcceptVisitor(Node node, INodePayloadVisitor visitor)
        {
            return visitor.VisitSourceRoot(node, this);
        }

        public override string ToString()
        {
            return $"{{ {nameof(SourceRoot)}-{FileName} }}";
        }
    }
}
