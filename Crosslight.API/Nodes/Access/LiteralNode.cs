namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// LiteralNode represents a literal in the language.
    /// </summary>
    public class LiteralNode : ValueNode
    {
        public override string Type => nameof(LiteralNode);
        public string LiteralValue { get; }
        public LiteralNode(string value)
        {
            LiteralValue = value;
        }
        public override string ToString()
        {
            return "LiteralNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
