using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// <see cref="BlockNode"/> represents the block of code.
    /// </summary>
    public class BlockNode : Node
    {
        public override string Type => nameof(BlockNode);
        public SyncedList<StatementNode, Node> Statements { get; protected set; }
        // scope
        public BlockNode()
        {
            Statements = new SyncedList<StatementNode, Node>(Children);
        }
        public override string ToString()
        {
            return "BlockNode";
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
