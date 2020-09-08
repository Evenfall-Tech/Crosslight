using System;

namespace Crosslight.API.Nodes.Metadata
{
    /// <summary>
    /// Represents compiler metadata or useful information.
    /// </summary>
    public class MetadataNode : Node
    {
        public override Type Type => typeof(MetadataNode);
        public string Metadata { get; }
        public MetadataNode(string metadata)
        {
            Metadata = metadata;
        }
        public override string ToString()
        {
            return Metadata;
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
