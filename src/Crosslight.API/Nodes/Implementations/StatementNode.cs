namespace Crosslight.API.Nodes.Implementations
{
    /// <summary>
    /// <see cref="StatementNode"/> represents the statement abstraction.
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
