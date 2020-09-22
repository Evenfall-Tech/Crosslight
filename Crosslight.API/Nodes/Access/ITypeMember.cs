using Crosslight.API.Nodes.Entities;

namespace Crosslight.API.Nodes.Access
{
    public interface ITypeMember
    {
        FunctionalTypeNode OwningType { get; set; }
    }
}
