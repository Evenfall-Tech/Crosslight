using Crosslight.API.Nodes.Interfaces;

namespace Crosslight.API.Nodes.Function
{
    public class DestructorNode : BaseMethodNode, ITypeMemberNode, INamedNode, IModifiedNode
    {
        public override string Type => nameof(DestructorNode);
        public DestructorNode(string name) : base(name)
        {
        }
    }
}
