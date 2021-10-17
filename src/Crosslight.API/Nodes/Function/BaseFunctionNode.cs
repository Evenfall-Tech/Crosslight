using Crosslight.API.Nodes.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// <see cref="BaseFunctionNode"/> is the parent node for function and method nodes.
    /// </summary>
    public abstract class BaseFunctionNode : EntityNode, INamedNode, IModifiedNode
    {
        public override string Type => nameof(BaseFunctionNode);
        public SyncedList<FunctionParameterNode, Node> Parameters { get; protected set; }
        private readonly SyncedProperty<FunctionBodyNode, Node> body;
        // TODO: Replace body with ExecutableNode <- FunctionBodyNode.
        public FunctionBodyNode Body
        {
            get => body.Value;
            protected set => body.Value = value;
        }
        public string Name { get; }
        public BaseFunctionNode(string name)
        {
            Parameters = new SyncedList<FunctionParameterNode, Node>(Children);
            body = new SyncedProperty<FunctionBodyNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return "FunctionNode";
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
