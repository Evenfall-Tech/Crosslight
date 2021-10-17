using Crosslight.API.Nodes.Implementations;
using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Interfaces.Entities
{
    public interface IGenericDefinitionProvider
    {
        SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; }
        SyncedList<TypeConstraintNode, Node> Constraints { get; }
        int Arity { get; }
    }
}
