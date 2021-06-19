namespace Crosslight.API.Nodes.Expressions
{
    /// <summary>
    /// <see cref="LiteralExpressionNode"/> represents the literal expression.
    /// </summary>
    public class LiteralExpressionNode : ExpressionNode
    {
        public override string Type => nameof(LiteralExpressionNode);

        public string Value { get; set; }
        // TODO: list all possible types
        // TODO: define constants for LiteralType
        public string LiteralType { get; set; }
        /// <summary>
        /// Example: 801ul - u and l
        /// </summary>
        public string[] LiteralModifiers { get; set; }

        public LiteralExpressionNode()
        {
        }
        public override string ToString()
        {
            return nameof(LiteralExpressionNode);
        }
    }
}
