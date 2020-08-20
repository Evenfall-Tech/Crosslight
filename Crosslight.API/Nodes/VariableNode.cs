using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// VariableNode represents the variable abstraction in the language.
    /// </summary>
    public class VariableNode : ValueNode
    {
        public override Type Type => typeof(VariableNode);
        public VariableNode()
        {
        }
        public override string ToString()
        {
            return "VariableNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
