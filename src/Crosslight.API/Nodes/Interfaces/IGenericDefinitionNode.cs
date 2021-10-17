using Crosslight.API.Nodes.Implementations;
using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Interfaces
{
    public interface IGenericDefinitionNode : INode
    {
        SyncedList<TypeConstraintNode, Node> Constraints { get; }
        SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; }
    }
}
