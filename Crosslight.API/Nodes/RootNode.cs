using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes
{
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
