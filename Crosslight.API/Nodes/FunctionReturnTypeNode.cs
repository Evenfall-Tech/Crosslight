using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// FunctionReturnTypeNode represents the return type of the function.
    /// </summary>
    public class FunctionReturnTypeNode : TypeNode
    {
        public SyncedList<AttributeNode, Node> ReturnTypeAttributes { get; protected set; }
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
