namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// <see cref="VariableNode"/> represents the variable abstraction in the language.
    /// </summary>
    public class VariableNode : ValueNode
    {
        public override string Type => nameof(VariableNode);
        public string Name { get; }
        public VariableNode(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return "VariableNode";
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
