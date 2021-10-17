namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// <see cref="LiteralNode"/> represents a literal in the language.
    /// </summary>
    public class LiteralNode : ValueNode
    {
        public override string Type => nameof(LiteralNode);
        public string LiteralValue { get; }
        public LiteralNode(string value)
        {
            LiteralValue = value;
        }
        public override string ToString()
        {
            return "LiteralNode";
        }
        // TODO: fix this.
        /*
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
        }*/
    }
}
