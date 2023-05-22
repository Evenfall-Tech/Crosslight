using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Methods
{
    public class ConversionOperatorDeclarationNode : BaseMethodDeclarationNode, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(ConversionOperatorDeclarationNode);
        private readonly SyncedProperty<ImplicitOrExplicitKeywordNode, Node> keyword;
        public ImplicitOrExplicitKeywordNode Keyword
        {
            get => keyword.Value;
            set => keyword.Value = value;
        }
        private readonly SyncedProperty<TypeReferenceNode, Node> returnType;
        public TypeReferenceNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public ConversionOperatorDeclarationNode()
        {
            keyword = new SyncedProperty<ImplicitOrExplicitKeywordNode, Node>(Children);
            returnType = new SyncedProperty<TypeReferenceNode, Node>(Children);
        }
    }
}
