using static Crosslight.Core.ILanguage;

namespace Crosslight.Core.Nodes
{
    /// <summary>
    /// Node payload container.
    /// </summary>
    public interface INodePayload
    {
        /// <summary>
        /// Type of the node payload.
        /// </summary>
        NodeType Type { get; }

        /// <summary>
        /// Convert the current payload implementation to a native representation.
        /// </summary>
        /// <param name="acquire">Delegate to allocate memory for the payload.</param>
        /// <returns>The native pointer, leading to the allocated native representation of the payload.</returns>
        /// <remarks>
        /// If a memory allocation error occurs anywhere apart from the initial payload memory allocation,
        /// the function returns a pointer to a partially converted structure. Otherwise, if the initial
        /// allocation failed, the function returns <c>0</c>.
        /// </remarks>
        nint ToPointer(AcquireDelegate? acquire);
    }
}
