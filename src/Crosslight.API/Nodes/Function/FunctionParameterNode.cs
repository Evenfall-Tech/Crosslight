using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// <see cref="FunctionParameterNode"/> represents parameters of the function.
    /// </summary>
    public class FunctionParameterNode : ValueNode
    {
        public override string Type => nameof(FunctionParameterNode);
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
