using Crosslight.API.Nodes.Implementations;
using Crosslight.API.Nodes.Implementations.Entities.Generics;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Interfaces.Entities
{
    public interface IGenericDefinitionProvider
    {
        SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; }
        SyncedList<TemplateTypeParameterConstraintClauseNode, Node> Constraints { get; }
        /// <summary>
        /// Number of generic arguments
        /// </summary>
        int Arity { get; }
    }
}
