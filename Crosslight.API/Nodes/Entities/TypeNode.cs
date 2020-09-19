using Crosslight.API.Nodes.Function;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// TypeNode represents the type abstraction in the language.
    /// E.g. in C# it is class/struct, for Java it is class,
    /// in C++ it is class/struct.
    /// </summary>
    public abstract class TypeNode : EntityNode
    {
        public override string Type => nameof(TypeNode);
        public SyncedList<MethodNode, Node> Methods { get; protected set; }
        public string Name { get; }
        public TypeNode(string name)
        {
            Methods = new SyncedList<MethodNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return "TypeNode";
        }
    }
}
