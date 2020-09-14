using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// LambdaNode represents the lambda abstraction in the language.
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
            set => returnType.Value = value;
        }
        public FunctionBodyNode Body
        {
            get => body.Value;
            set => body.Value = value;
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
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
