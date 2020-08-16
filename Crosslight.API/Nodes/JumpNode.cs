using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// JumpNode is an abstract class that represents control flow operations.
    /// </summary>
    public abstract class JumpNode : StatementNode
    {
        public JumpNode()
        {
        }
        public override string ToString()
        {
            return "JumpNode";
        }
    }
}
