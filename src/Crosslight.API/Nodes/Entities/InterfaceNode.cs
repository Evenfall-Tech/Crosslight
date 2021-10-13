using Crosslight.API.Nodes.Interfaces;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="InterfaceNode"/> represents the interface abstraction in the language.
    /// </summary>
    public class InterfaceNode : FunctionalTypeNode, IAttributedNode, IModifiedNode, IGenericDefinitionNode, INamedNode
    {
        public override string Type => nameof(InterfaceNode);
        public InterfaceNode(string name) : base(name)
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
