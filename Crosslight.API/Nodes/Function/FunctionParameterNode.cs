using Crosslight.API.Nodes.Access;
using Crosslight.Common.Util;
using System;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// FunctionParameterNode represents parameters of the function.
    /// </summary>
    public class FunctionParameterNode : ValueNode // or better keep TypeNode as field?
    {
        public override Type Type => typeof(FunctionParameterNode);
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public string Name { get; }
        public FunctionParameterNode(string name)
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return "FunctionParameterNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
