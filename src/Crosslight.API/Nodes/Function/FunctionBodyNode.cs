using Crosslight.API.Nodes.Control;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// <see cref="FunctionBodyNode"/> represents the body of the function.
    /// </summary>
    public class FunctionBodyNode : Node
    {
        public override string Type => nameof(FunctionBodyNode);
        private readonly SyncedProperty<BlockNode, Node> block;
        public BlockNode Block
        {
            get => block.Value;
            protected set => block.Value = value;
        }
        public FunctionBodyNode()
        {
            block = new SyncedProperty<BlockNode, Node>(Children);
        }
        public override string ToString()
        {
            return "FunctionBodyNode";
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
