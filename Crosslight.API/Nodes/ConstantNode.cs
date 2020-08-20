using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ConstantNode represents the constant abstraction in the language.
    /// </summary>
    public class ConstantNode : ValueNode
    {
        public override Type Type => typeof(ConstantNode);
        public ConstantNode()
        {
        }
        public override string ToString()
        {
            return "ConstantNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
