using Crosslight.API.Nodes.Implementations.Entities.Generics;
using Crosslight.API.Nodes.Implementations.Entities.Methods;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Types
{
    /// <summary>
    /// <see cref="FunctionalTypeDeclarationNode"/> is reserved for the functional type abstraction in the language.
    /// </summary>
    public abstract class FunctionalTypeDeclarationNode : BaseTypeDeclarationNode, IAttributesProvider, IModifiersProvider, IGenericDefinitionProvider, IIdentifierProvider
    {
        public override string Type => nameof(FunctionalTypeDeclarationNode);
        public SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; protected set; }
        public SyncedList<TemplateTypeParameterConstraintClauseNode, Node> Constraints { get; protected set; }
        public int Arity { get; protected set; }
        public SyncedList<BaseMethodDeclarationNode, Node> Methods { get; protected set; }

        public FunctionalTypeDeclarationNode(string identifier) : base(identifier)
        {
            TypeParameters = new SyncedList<TemplateTypeParameterNode, Node>(Children);
            Constraints = new SyncedList<TemplateTypeParameterConstraintClauseNode, Node>(Children);
            Methods = new SyncedList<BaseMethodDeclarationNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
