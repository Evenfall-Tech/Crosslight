namespace Crosslight.Core.Nodes;

/// <summary>
/// Type of the given node.
/// </summary>
public enum NodeType : uint
{
    /// <summary>
    /// No payload given.
    /// </summary>
    None = 0,
    /// <summary>
    /// Textual source file root.
    /// </summary>
    SourceRoot = 1,
    /// <summary>
    /// Declaration scope.
    /// </summary>
    Scope = 2,
    /// <summary>
    /// Complex type stored on the heap.
    /// </summary>
    HeapType = 3,
}
