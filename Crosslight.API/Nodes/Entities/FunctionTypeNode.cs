using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Entities
{
    public class FunctionTypeNode : EntityNode
    {
        public override string Type => nameof(FunctionTypeNode);
        public FunctionTypeNode(string name)
        {
            // TODO: add FunctionType properties.
        }
        public override string ToString()
        {
            return Type;
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
