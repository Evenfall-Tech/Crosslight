using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Nodes.Implementations.Entities.Types;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Function
{
    /// <summary>
    /// <see cref="FunctionReturnTypeNode"/> represents the return type of the function.
    /// </summary>
    public class FunctionReturnTypeNode : Node
    {
        public override string Type => nameof(FunctionReturnTypeNode);
        public SyncedList<AttributeNode, Node> ReturnTypeAttributes { get; protected set; }
        // TODO: rewrite with a reference
        public FunctionalTypeDeclarationNode ReturnType { get; protected set; }
        public FunctionReturnTypeNode()
        {
            ReturnTypeAttributes = new SyncedList<AttributeNode, Node>(Children);
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
