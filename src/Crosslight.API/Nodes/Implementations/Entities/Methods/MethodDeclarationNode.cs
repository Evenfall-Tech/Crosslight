using Crosslight.API.Nodes.Implementations.Entities.Generics;
using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Methods
{
    public class MethodDeclarationNode : BaseMethodDeclarationNode, IIdentifierProvider, IModifiersProvider, IAttributesProvider, IGenericDefinitionProvider
    {
        public override string Type => nameof(MethodDeclarationNode);
        public SyncedList<TemplateTypeParameterConstraintClauseNode, Node> Constraints { get; protected set; }
        public SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; protected set; }
        private readonly SyncedProperty<TypeReferenceNode, Node> explicitInterfaceSpecifier;
        public TypeReferenceNode ExplicitInterfaceSpecifier
        {
            get => explicitInterfaceSpecifier.Value;
            set => explicitInterfaceSpecifier.Value = value;
        }
        private readonly SyncedProperty<TypeReferenceNode, Node> returnType;
        public TypeReferenceNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public int Arity => TypeParameters.Count;

        public string Identifier { get; }
        public MethodDeclarationNode(string identifier)
        {
            Identifier = identifier;
            explicitInterfaceSpecifier = new SyncedProperty<TypeReferenceNode, Node>(Children);
            Constraints = new SyncedList<TemplateTypeParameterConstraintClauseNode, Node>(Children);
            TypeParameters = new SyncedList<TemplateTypeParameterNode, Node>(Children);
            returnType = new SyncedProperty<TypeReferenceNode, Node>(Children);
        }
    }
}
