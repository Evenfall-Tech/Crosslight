namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// <see cref="ConstantNode"/> represents the constant abstraction in the language.
    /// </summary>
    public class ConstantNode : ValueNode
    {
        public override string Type => nameof(ConstantNode);
        public string Name { get; }
        public ConstantNode(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return "ConstantNode";
        }
        // TODO: fix this.
        /*public override object AcceptVisitor(IVisitor visitor)
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
