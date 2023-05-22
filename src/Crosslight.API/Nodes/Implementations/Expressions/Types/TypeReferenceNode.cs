namespace Crosslight.API.Nodes.Implementations.Expressions.Types
{
    public abstract class TypeReferenceNode : Node // TODO: inheritance
    {
        public override string Type => nameof(TypeReferenceNode);
        public override string ToString()
        {
            return Type;
        }
    }
}
