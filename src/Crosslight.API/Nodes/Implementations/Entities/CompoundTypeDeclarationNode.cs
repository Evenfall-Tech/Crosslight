using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    /// <summary>
    /// <see cref="CompoundTypeDeclarationNode"/> abstract node represents types
    /// that can have inner declarations.
    /// E.g. in C# it is class/struct, for Java it is class,
    /// in C++ it is class/struct.
    /// </summary>
    public abstract class CompoundTypeDeclarationNode : FunctionalTypeDeclarationNode, IAttributesProvider, IModifiersProvider, IGenericDefinitionProvider, IIdentifierProvider
    {
        public override string Type => nameof(CompoundTypeDeclarationNode);
        public SyncedList<MemberDeclarationNode, Node> Members { get; protected set; }
        public CompoundTypeDeclarationNode(string identifier) : base(identifier)
        {
            Members = new SyncedList<MemberDeclarationNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
