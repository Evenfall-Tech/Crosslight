namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// <see cref="ModifierNode"/> represents the modifier (public, abstract, virtual).
    /// </summary>
    public class ModifierNode : Node
    {
        public override string Type => nameof(ModifierNode);
        // maybe enum with modifiers
        public ModifierNode()
        {
        }
        public override string ToString()
        {
            return "ModifierNode";
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
