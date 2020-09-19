namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// ClassNode represents the class abstraction in the language.
    /// </summary>
    public class ClassNode : BaseTypeNode
    {
        public override string Type => nameof(ClassNode);
        public ClassNode(string name) : base(name)
        {
        }
        public override string ToString()
        {
            return "ClassNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }
    }
}
