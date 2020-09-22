using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    public abstract class CompoundTypeNode : FunctionalTypeNode
    {
        public override string Type => nameof(FunctionalTypeNode);
        public SyncedList<FieldNode, Node> Fields { get; protected set; }
        public SyncedList<EntityNode, Node> InnerEntities { get; protected set; }
        public CompoundTypeNode(string name) : base(name)
        {
            Fields = new SyncedList<FieldNode, Node>(Children);
            InnerEntities = new SyncedList<EntityNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
