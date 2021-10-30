using Crosslight.API.Nodes.Implementations.Control;
using Crosslight.API.Nodes.Implementations.Entities.Methods;
using Crosslight.API.Nodes.Implementations.Expressions;
using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Function
{
    /// <summary>
    /// <see cref="LambdaNode"/> represents the lambda abstraction in the language.
    /// </summary>
    public class LambdaNode : Node
    {
        // TODO: fix this mess, inherit from anonymous expression
        public override string Type => nameof(LambdaNode);
        public SyncedList<ParameterNode, Node> Parameters { get; protected set; }
        // TODO: in C++11 lambdas might have return type specified
        private readonly SyncedProperty<TypeReferenceNode, Node> returnType;
        private readonly SyncedProperty<BlockNode, Node> body;
        private readonly SyncedProperty<ExpressionNode, Node> expressionBody;
        public TypeReferenceNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
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
        public LambdaNode()
        {
            Parameters = new SyncedList<ParameterNode, Node>(Children);
            returnType = new SyncedProperty<TypeReferenceNode, Node>(Children);
            body = new SyncedProperty<BlockNode, Node>(Children);
            expressionBody = new SyncedProperty<ExpressionNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
        // TODO: fix this.
        /*public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }*/
    }
}
