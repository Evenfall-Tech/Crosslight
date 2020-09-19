namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// InterfaceNode represents the interface abstraction in the language.
    /// </summary>
    public class InterfaceNode : TypeNode
    {
        public override string Type => nameof(InterfaceNode);
        public InterfaceNode(string name) : base(name)
        {
        }
        public override string ToString()
        {
            return "InterfaceNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
