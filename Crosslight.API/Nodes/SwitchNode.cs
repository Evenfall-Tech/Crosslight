using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// SwitchNode represents the switch operator.
    /// </summary>
    public class SwitchNode : StatementNode
    {
        public SwitchNode()
        {
        }
        public override string ToString()
        {
            return "SwitchNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
