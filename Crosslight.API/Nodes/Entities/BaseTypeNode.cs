using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    public abstract class BaseTypeNode : TypeNode
    {
        public override string Type => nameof(TypeNode);
        public SyncedList<FieldNode, Node> Fields { get; protected set; }
        public SyncedList<EntityNode, Node> InnerEntities { get; protected set; }
        public BaseTypeNode(string name) : base(name)
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
