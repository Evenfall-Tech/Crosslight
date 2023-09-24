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
        nint ToPointer(AcquireDelegate? acquire);
    }
}
