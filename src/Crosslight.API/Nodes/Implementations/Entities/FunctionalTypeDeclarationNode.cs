using Crosslight.API.Nodes.Implementations.Function;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    /// <summary>
    /// <see cref="FunctionalTypeDeclarationNode"/> represents the functional type abstraction in the language.
    /// </summary>
    public abstract class FunctionalTypeDeclarationNode : BaseTypeDeclarationNode, IAttributesProvider, IModifiersProvider, IGenericDefinitionProvider, IIdentifierProvider
    {
        public override string Type => nameof(FunctionalTypeDeclarationNode);
        public SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; protected set; }
        public SyncedList<TypeConstraintNode, Node> Constraints { get; protected set; }
        public int Arity { get; protected set; }
        public SyncedList<BaseMethodNode, Node> Methods { get; protected set; }

        public FunctionalTypeDeclarationNode(string identifier) : base(identifier)
        {
            TypeParameters = new SyncedList<TemplateTypeParameterNode, Node>(Children);
            Constraints = new SyncedList<TypeConstraintNode, Node>(Children);
            Methods = new SyncedList<BaseMethodNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
