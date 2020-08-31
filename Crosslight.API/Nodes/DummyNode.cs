using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes
{
    public class DummyNode : Node
    {
        public override Type Type => typeof(DummyNode);
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
    }
}
