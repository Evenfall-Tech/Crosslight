using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// IfNode represents the if-else statement.
    /// </summary>
    public class IfNode : StatementNode
    {
        public override Type Type => typeof(IfNode);
        private readonly SyncedProperty<ExpressionNode, Node> condition;
        private readonly SyncedProperty<BlockNode, Node> ifBlock;
        private readonly SyncedProperty<BlockNode, Node> elseBlock;
        public SyncedList<ExpressionNode, Node> ElseIfConditions { get; protected set; }
        public SyncedList<BlockNode, Node> ElseIfBlocks{ get; protected set; }
        public ExpressionNode Condition
        {
            get => condition.Value;
            set => condition.Value = value;
        }
        public BlockNode IfBlock
        {
            get => ifBlock.Value;
            set => ifBlock.Value = value;
        }
        public BlockNode ElseBlock
        {
            get => elseBlock.Value;
            set => elseBlock.Value = value;
        }
        public IfNode()
        {
            condition= new SyncedProperty<ExpressionNode, Node>(Children);
            ifBlock = new SyncedProperty<BlockNode, Node>(Children);
            elseBlock = new SyncedProperty<BlockNode, Node>(Children);
            ElseIfConditions = new SyncedList<ExpressionNode, Node>(Children);
            ElseIfBlocks= new SyncedList<BlockNode, Node>(Children);
        }
        public override string ToString()
        {
            return "IfNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
