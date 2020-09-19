using Crosslight.API.Nodes.Entities;

namespace Crosslight.API.Nodes.Access
{
    public interface ITypeMember
    {
        TypeNode OwningType { get; set; }
    }
}
