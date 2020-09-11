using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes
{
    public class RootNode : Node
    {
        public override Type Type => typeof(RootNode);
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
    }
}
