namespace Crosslight.API.Nodes.Implementations.Entities.Generics
{
    /// <summary>
    /// <see cref="ConstructorConstraintNode"/> represents the : new() constraint.
    /// </summary>
    public class ConstructorConstraintNode : TemplateTypeParameterConstraintNode
    {
        public override string Type => nameof(ConstructorConstraintNode);
        // TODO: add support for specifying custom constructors with parameters.
        public override string ToString()
        {
            return Type;
        }
    }
}
