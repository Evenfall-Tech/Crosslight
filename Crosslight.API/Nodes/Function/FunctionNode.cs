using Crosslight.API.Nodes.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// FunctionNode represents the function abstraction in the language.
    /// </summary>
    public class FunctionNode : EntityNode
    {
        public override string Type => nameof(FunctionNode);
        public SyncedList<FunctionParameterNode, Node> Parameters { get; protected set; }
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
        public string Name { get; }
        public FunctionNode(string name)
        {
            Parameters = new SyncedList<FunctionParameterNode, Node>(Children);
            returnType = new SyncedProperty<FunctionReturnTypeNode, Node>(Children);
            body = new SyncedProperty<FunctionBodyNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return "FunctionNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
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
        }
    }
}
