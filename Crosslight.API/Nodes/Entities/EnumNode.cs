namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// EnumNode represents the enum abstraction in the language.
    /// </summary>
    public class EnumNode : EntityNode
    {
        public override string Type => nameof(EnumNode);
        public EnumNode(string name)
        {
        }
        public override string ToString()
        {
            return Type;
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
