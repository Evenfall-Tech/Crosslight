using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Generics
{
    /// <summary>
    /// <see cref="TemplateTypeParameterConstraintClauseNode"/> represents a
    /// specialization constraint on a type.
    /// E.g. where T : BaseTypes.
    /// </summary>
    public class TemplateTypeParameterConstraintClauseNode : Node, IIdentifierProvider
    {
        public override string Type => nameof(TemplateTypeParameterConstraintClauseNode);
        public SyncedList<TemplateTypeParameterConstraintNode, Node> Constraints { get; protected set; }
        public string Identifier { get; }
        public TemplateTypeParameterConstraintClauseNode(string identifier)
        {
            Constraints = new SyncedList<TemplateTypeParameterConstraintNode, Node>(Children);
            Identifier = identifier;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
