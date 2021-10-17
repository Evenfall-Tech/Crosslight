using Crosslight.API.Nodes.Implementations.Entities;

namespace Crosslight.API.Nodes.Interfaces
{
    /// <summary>
    /// <see cref="ITypeMemberNode"/> shows that a node has a parent owner.
    /// </summary>
    public interface ITypeMemberNode : INode
    {
        FunctionalTypeNode OwningType { get; }
    }
}
