using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Function
{
    /// <summary>
    /// <see cref="FunctionParameterNode"/> represents parameters of the function.
    /// </summary>
    public class FunctionParameterNode : VariableDeclarationNode // TODO: this was value node
    {
        public override string Type => nameof(FunctionParameterNode);
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public FunctionParameterNode(string identifier) : base(identifier)
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
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
