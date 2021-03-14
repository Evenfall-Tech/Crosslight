namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// <see cref="BreakNode"/> represents the break statement.
    /// </summary>
    public class BreakNode : JumpNode
    {
        public override string Type => nameof(BreakNode);
        public BreakNode()
        {
        }
        public override string ToString()
        {
            return "BreakNode";
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
