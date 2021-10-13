using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    public class ConstructorNode : BaseMethodNode, ITypeMemberNode, INamedNode, IModifiedNode
    {
        public override string Type => nameof(ConstructorNode);
        public SyncedList<ExpressionNode, Node> Initializers { get; protected set; }
        public ConstructorNode(string name) : base(name)
        {
            Initializers = new SyncedList<ExpressionNode, Node>(Children);
        }
    }
}
