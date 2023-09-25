using Crosslight.Core.Nodes;
using Crosslight.Core.Utilities;
using System.Runtime.InteropServices;
using static Crosslight.Core.ILanguage;

namespace Crosslight.src.Core.Nodes
{
    /// <summary>
    /// Textual source file root.
    /// </summary>
    public struct SourceRoot : INodePayload
    {
        private struct SourceRootImported
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
            SourceRootImported imported = Marshal.PtrToStructure<SourceRootImported>(pointer);

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
            nint pointer = acquire(Marshal.SizeOf<SourceRootImported>());

            if (pointer == 0)
            {
                return 0;
            }

            nint fileName = FileName == null
                ? 0
                : Utf8String.ToPointer(FileName, acquire);

            SourceRootImported imported = new()
            {
                FileName = fileName,
            };

            Marshal.StructureToPtr(imported, pointer, false);

            return pointer;
        }

        public readonly void AcceptVisitor(INodePayloadVisitor visitor)
        {
            visitor.VisitSourceRoot(this);
        }
    }
}
