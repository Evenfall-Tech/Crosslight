using Crosslight.API.Nodes.Interfaces;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="StructNode"/> represents the struct abstraction in the language.
    /// </summary>
    public class StructNode : CompoundTypeNode, IAttributedNode, IModifiedNode, IGenericDefinitionNode, INamedNode
    {
        public override string Type => nameof(StructNode);
        public StructNode(string name) : base(name)
        {
        }
        public override string ToString()
        {
            return Type;
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }
    }
}
