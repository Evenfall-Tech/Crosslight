namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// <see cref="BlockStatementNode"/> represents something.
    /// </summary>
    public class BlockStatementNode : StatementNode
    {
        public override string Type => nameof(BlockStatementNode);
        public BlockStatementNode()
        {
        }
        public override string ToString()
        {
            return "BlockStatementNode";
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
