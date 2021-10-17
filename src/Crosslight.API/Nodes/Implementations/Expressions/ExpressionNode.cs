namespace Crosslight.API.Nodes.Implementations.Expressions
{
    /// <summary>
    /// <see cref="ExpressionNode"/> represents the expression.
    /// </summary>
    public abstract class ExpressionNode : Node
    {
        public override string Type => nameof(ExpressionNode);
        public ExpressionNode()
        {
        }
        public override string ToString()
        {
            return nameof(ExpressionNode);
        }
    }
}
