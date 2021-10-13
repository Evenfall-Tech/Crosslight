using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// <see cref="FunctionReturnTypeNode"/> represents the return type of the function.
    /// </summary>
    public class FunctionReturnTypeNode : Node
    {
        public override string Type => nameof(FunctionReturnTypeNode);
        public SyncedList<AttributeNode, Node> ReturnTypeAttributes { get; protected set; }
        public FunctionalTypeNode ReturnType { get; protected set; }
        public FunctionReturnTypeNode()
        {
            ReturnTypeAttributes = new SyncedList<AttributeNode, Node>(Children);
        }
        public override string ToString()
        {
            return "FunctionReturnTypeNode";
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
