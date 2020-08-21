using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// StatementNode represents the statement abstraction.
    /// </summary>
    public abstract class StatementNode : Node
    {
        public override Type Type => typeof(StatementNode);
        public StatementNode()
        {
        }
        public override string ToString()
        {
            return "StatementNode";
        }
    }
}
