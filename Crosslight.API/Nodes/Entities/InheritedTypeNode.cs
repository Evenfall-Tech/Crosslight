using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    public abstract class InheritedTypeNode : EntityNode
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
