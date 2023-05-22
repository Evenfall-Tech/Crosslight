using Crosslight.API.Nodes.Interfaces;

namespace Crosslight.API.Nodes.Implementations.Entities.Generics
{
    /// <summary>
    /// <see cref="CompoundTypeConstraintNode"/> represents : class?, struct? and others.
    /// </summary>
    public class CompoundTypeConstraintNode : TemplateTypeParameterConstraintNode, IIdentifierProvider
    {
        public override string Type => nameof(CompoundTypeConstraintNode);
        public bool Nullable { get; }
        public string Identifier { get; }
        public CompoundTypeConstraintNode(string identifier, bool nullable)
        {
            Identifier = identifier;
            Nullable = nullable;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
