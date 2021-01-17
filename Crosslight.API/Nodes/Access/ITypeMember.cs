using Crosslight.API.Nodes.Entities;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// <see cref="ITypeMember"/> shows that a node has a parent owner.
    /// </summary>
    public interface ITypeMember
    {
        FunctionalTypeNode OwningType { get; set; }
    }
}
