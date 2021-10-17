using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Function
{
    /// <summary>
    /// <see cref="BaseFunctionNode"/> is the parent node for function and method nodes.
    /// </summary>
    public abstract class BaseFunctionNode : EntityNode, IIdentifierProvider, IModifiersProvider, IAttributesProvider
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
        public string Identifier { get; }
        public BaseFunctionNode(string identifier)
        {
            Parameters = new SyncedList<FunctionParameterNode, Node>(Children);
            body = new SyncedProperty<FunctionBodyNode, Node>(Children);
            Identifier = identifier;
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
