using Crosslight.API.Nodes.Implementations.Control;
using Crosslight.API.Nodes.Implementations.Expressions;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Methods
{
    /// <summary>
    /// <see cref="BaseMethodDeclarationNode"/> represents the method abstraction in the language.
    /// </summary>
    public abstract class BaseMethodDeclarationNode : MemberDeclarationNode, IModifiersProvider, IAttributesProvider
    {
        public override string Type => nameof(BaseMethodDeclarationNode);
        public SyncedList<ParameterNode, Node> Parameters { get; protected set; }
        private readonly SyncedProperty<BlockNode, Node> body;
        private readonly SyncedProperty<ExpressionNode, Node> expressionBody;
        public BlockNode Body
        {
            get => body.Value;
            protected set => body.Value = value;
        }
        public ExpressionNode ExpressionBody
        {
            get => expressionBody.Value;
            protected set => expressionBody.Value = value;
        }
        public BaseMethodDeclarationNode()
        {
            Parameters = new SyncedList<ParameterNode, Node>(Children);
            body = new SyncedProperty<BlockNode, Node>(Children);
            expressionBody = new SyncedProperty<ExpressionNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
