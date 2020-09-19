using System;

namespace Crosslight.API.Nodes.Metadata
{
    public class DummyNode : Node
    {
        public override string Type => nameof(DummyNode);
        public string Data { get; }
        public DummyNode(string data)
        {
            Data = data;
        }
        public override string ToString()
        {
            return Data;
        }
        public override object AcceptVisitor(IVisitor visitor)
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
        }
    }
}
