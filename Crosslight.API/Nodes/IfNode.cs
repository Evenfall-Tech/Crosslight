using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// IfNode represents the if-else statement.
    /// </summary>
    public class IfNode : StatementNode
    {
        public IfNode()
        {
        }
        public override string ToString()
        {
            return "IfNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
