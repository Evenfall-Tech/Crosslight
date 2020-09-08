using System;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// JumpNode is an abstract class that represents control flow operations.
    /// </summary>
    public abstract class JumpNode : StatementNode
    {
        public override Type Type => typeof(JumpNode);
        public JumpNode()
        {
        }
        public override string ToString()
        {
            return "JumpNode";
        }
    }
}
