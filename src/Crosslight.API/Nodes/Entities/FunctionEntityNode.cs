namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="FunctionEntityNode"/> represents C# delegate declaration.
    /// </summary>
    public class FunctionEntityNode : EntityNode
    {
        public override string Type => nameof(FunctionEntityNode);
        public FunctionEntityNode(string name)
        {
            // TODO: add FunctionType properties.
        }
        public override string ToString()
        {
            return Type;
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
