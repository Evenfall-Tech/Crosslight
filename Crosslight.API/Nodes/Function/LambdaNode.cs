using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// <see cref="LambdaNode"/> represents the lambda abstraction in the language.
    /// </summary>
    public class LambdaNode : Node
    {
        public override string Type => nameof(LambdaNode);
        public SyncedList<FunctionParameterNode, Node> Parameters { get; protected set; }
        // TODO: in C++11 lambdas might have return type specified
        private readonly SyncedProperty<FunctionReturnTypeNode, Node> returnType;
        private readonly SyncedProperty<FunctionBodyNode, Node> body;
        public FunctionReturnTypeNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public FunctionBodyNode Body
        {
            get => body.Value;
            protected set => body.Value = value;
        }
        public LambdaNode()
        {
            Parameters = new SyncedList<FunctionParameterNode, Node>(Children);
            returnType = new SyncedProperty<FunctionReturnTypeNode, Node>(Children);
            body = new SyncedProperty<FunctionBodyNode, Node>(Children);
        }
        public override string ToString()
        {
            return "LambdaNode";
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
