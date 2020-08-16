using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// BlockNode represents the block of code.
    /// </summary>
    public class BlockNode : Node
    {
        // statement list
        // scope
        public BlockNode()
        {
        }
        public override string ToString()
        {
            return "BlockNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
