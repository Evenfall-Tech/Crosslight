namespace Crosslight.API.Nodes.Metadata
{
    /// <summary>
    /// <see cref="MetadataNode"/> represents compiler metadata or useful information.
    /// </summary>
    public class MetadataNode : Node
    {
        public override string Type => nameof(MetadataNode);
        public string Metadata { get; }
        public MetadataNode(string metadata)
        {
            Metadata = metadata;
        }
        public override string ToString()
        {
            return Metadata;
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
