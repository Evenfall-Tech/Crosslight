using Crosslight.Core.Nodes;
using Crosslight.Core.Utilities;
using System.Runtime.InteropServices;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Core;

public class Node
{
    [StructLayout(LayoutKind.Sequential)]
    private struct NodeImported
    {
        public nint Payload;
        public nuint PayloadType;
        public nint Parent;
        public nint Children;
        public nuint ChildCount;
    }

    /// <summary>
    /// Payload node containing specialized fields.
    /// </summary>
    public INodePayload? Payload { get; }

    /// <summary>
    /// Type of the payload contained in this node.
    /// </summary>
    public NodeType Type => Payload?.Type ?? NodeType.None;

    /// <summary>
    /// Parent of the current node. Used for faster traversing.
    /// </summary>
    public Node? Parent { get; private set; }

    /// <summary>
    /// Children of the current node.
    /// </summary>
    public IReadOnlyCollection<Node>? Children { get; private set; }

    /// <summary>
    /// Create a new node instance as a parent of some children.
    /// </summary>
    /// <param name="children">Initialized set of child nodes to connect to this parent.</param>
    /// <param name="payload">The payload of the created node.</param>
    public Node(INodePayload payload, IEnumerable<Node>? children)
    {
        if (children != null)
        {
            foreach (var child in children)
            {
                child.Parent = this;
            }

            Children = children.ToList();
        }

        Payload = payload;
    }

    /// <summary>
    /// Create a new node instance from a native representation.
    /// </summary>
    /// <param name="pointer">The native pointer to a node.</param>
    public Node(nint pointer)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Convert this node and all its children to a native pointer.
    /// </summary>
    /// <param name="acquire">Delegate to allocate memory for the node, its contents and children.</param>
    /// <returns>The native pointer, leading to the allocated native representation of the node.</returns>
    public nint ToPointer(AcquireDelegate? acquire = null)
    {
        acquire ??= Marshal.AllocCoTaskMem;
        throw new NotImplementedException();
        return 0;
    }

    /// <summary>
    /// Get the strongly-typed payload of the current node.
    /// </summary>
    /// <typeparam name="TPayload">Type of the payload to get.</typeparam>
    /// <returns>The payload instance, if it exists and the type is correct. Otherwise <see langword="null"/>.</returns>
    public TPayload? GetPayload<TPayload>() where TPayload : struct, INodePayload
    {
        if (Payload == null)
        {
            return null;
        }

        if (Payload is TPayload payload)
        {
            return payload;
        }

        return null;
    }
}
