using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// FunctionBodyNode represents the body of the function.
    /// </summary>
    public class FunctionBodyNode : Node
    {
        public override Type Type => typeof(FunctionBodyNode);
        private readonly SyncedProperty<BlockNode, Node> block;
        public BlockNode Block
        {
            get => block.Value;
            set => block.Value = value;
        }
        public FunctionBodyNode()
        {
            block = new SyncedProperty<BlockNode, Node>(Children);
        }
        public override string ToString()
        {
            return "FunctionBodyNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
