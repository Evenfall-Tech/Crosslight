using System;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// StatementNode represents the statement abstraction.
    /// </summary>
    public abstract class StatementNode : Node
    {
        public override string Type => nameof(StatementNode);
        public StatementNode()
        {
        }
        public override string ToString()
        {
            return "StatementNode";
        }
    }
}
