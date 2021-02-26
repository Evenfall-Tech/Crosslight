using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="InheritedTypeNode"/> abstract node represents a type
    /// that has one or more base types.
    /// Similar to Base Type Declaration Syntax.
    /// </summary>
    public abstract class InheritedTypeNode : EntityNode, IAttributedNode, IModifiedNode, IGenericDefinitionNode, INamedNode
    {
        public override string Type => nameof(InheritedTypeNode);
        public string Name { get; }
        public SyncedList<InheritedTypeNode, Node> BaseTypes { get; protected set; }
        public SyncedList<TypeConstraintNode, Node> Constraints { get; protected set; }
        public SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; protected set; }
        public InheritedTypeNode(string name)
        {
            BaseTypes = new SyncedList<InheritedTypeNode, Node>(Children);
            Constraints = new SyncedList<TypeConstraintNode, Node>(Children);
            TypeParameters = new SyncedList<TemplateTypeParameterNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
