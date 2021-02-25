namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// <see cref="ContinueNode"/> represents the continue statement.
    /// </summary>
    public class ContinueNode : JumpNode
    {
        public override string Type => nameof(ContinueNode);
        public ContinueNode()
        {
        }
        public override string ToString()
        {
            return "ContinueNode";
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
