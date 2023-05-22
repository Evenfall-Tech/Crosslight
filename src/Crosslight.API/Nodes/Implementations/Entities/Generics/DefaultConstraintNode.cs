namespace Crosslight.API.Nodes.Implementations.Entities.Generics
{
    public class DefaultConstraintNode : TemplateTypeParameterConstraintNode
    {
        public override string Type => nameof(DefaultConstraintNode);
        public override string ToString()
        {
            return Type;
        }
    }
}
