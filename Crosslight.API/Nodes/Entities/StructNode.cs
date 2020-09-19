namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// StructNode represents the struct abstraction in the language.
    /// </summary>
    public class StructNode : TypeNode
    {
        public override string Type => nameof(StructNode);
        public StructNode(string name) : base(name)
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
