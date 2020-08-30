using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// FunctionReturnTypeNode represents the return type of the function.
    /// </summary>
    public class FunctionReturnTypeNode : Node
    {
        public override Type Type => typeof(FunctionReturnTypeNode);
        public SyncedList<AttributeNode, Node> ReturnTypeAttributes { get; protected set; }
        public TypeNode ReturnType { get; protected set; }
        public FunctionReturnTypeNode()
        {
            ReturnTypeAttributes = new SyncedList<AttributeNode, Node>(Children);
        }
        public override string ToString()
        {
            return "FunctionReturnTypeNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
