using Crosslight.API.Nodes.Function;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="FunctionalTypeNode"/> represents the type abstraction in the language.
    /// E.g. in C# it is class/struct, for Java it is class,
    /// in C++ it is class/struct.
    /// </summary>
    public abstract class FunctionalTypeNode : InheritedTypeNode, IAttributedNode, IModifiedNode, IGenericDefinitionNode, INamedNode
    {
        public override string Type => nameof(FunctionalTypeNode);
        public SyncedList<BaseMethodNode, Node> Methods { get; protected set; }
        public FunctionalTypeNode(string name) : base(name)
        {
            Methods = new SyncedList<BaseMethodNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
