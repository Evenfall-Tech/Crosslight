namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="EnumNode"/> represents the enum abstraction in the language.
    /// </summary>
    public class EnumNode : InheritedTypeNode
    {
        public override string Type => nameof(EnumNode);
        public EnumNode(string name) : base(name)
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
