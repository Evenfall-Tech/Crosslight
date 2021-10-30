namespace Crosslight.API.Nodes.Implementations.Entities.Generics
{
    public abstract class TemplateTypeParameterConstraintNode : Node
    {
        public override string Type => nameof(TemplateTypeParameterConstraintNode);
        public override string ToString()
        {
            return Type;
        }
    }
}
