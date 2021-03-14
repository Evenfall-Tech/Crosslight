using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="TypeConstraintNode"/> represents a
    /// specialization constraint on a type.
    /// E.g. where T : BaseTypes.
    /// </summary>
    public class TypeConstraintNode : Node
    {
        // TODO: complete constraint class.
        public override string Type => nameof(TypeConstraintNode);
        private readonly SyncedProperty<TemplateTypeParameterNode, Node> typeParemeter;
        public SyncedList<InheritedTypeNode, Node> BaseTypes { get; protected set; }
        public TemplateTypeParameterNode TypeParemeter
        {
            get => typeParemeter.Value;
            protected set => typeParemeter.Value = value;
        }
        public string Name { get; protected set; }
        public TypeConstraintNode(string name)
        {
            typeParemeter = new SyncedProperty<TemplateTypeParameterNode, Node>(Children);
            BaseTypes = new SyncedList<InheritedTypeNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
