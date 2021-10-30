using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Methods
{
    public class OperatorDeclarationNode : BaseMethodDeclarationNode, IIdentifierProvider, IModifiersProvider, IAttributesProvider
    {
        public override string Type => nameof(OperatorDeclarationNode);
        private readonly SyncedProperty<TypeReferenceNode, Node> returnType;
        public TypeReferenceNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public string Identifier { get; }
        public OperatorDeclarationNode(string identifier)
        {
            Identifier = identifier;
            returnType = new SyncedProperty<TypeReferenceNode, Node>(Children);
        }
    }
}
