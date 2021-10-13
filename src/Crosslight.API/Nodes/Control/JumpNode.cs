namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// <see cref="JumpNode"/> is an abstract class that represents control flow operations.
    /// </summary>
    public abstract class JumpNode : StatementNode
    {
        public override string Type => nameof(JumpNode);
        public JumpNode()
        {
        }
        public override string ToString()
        {
            return "JumpNode";
        }
    }
}
