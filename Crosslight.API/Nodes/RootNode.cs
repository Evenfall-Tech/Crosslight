namespace Crosslight.API.Nodes
{
    /// <summary>
    /// <see cref="RootNode"/> represents a node that is
    /// the root of the syntax tree.
    /// </summary>
    public class RootNode : Node
    {
        public override string Type => nameof(RootNode);
        public RootNode()
        {
        }
        public override string ToString()
        {
            return nameof(RootNode);
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
