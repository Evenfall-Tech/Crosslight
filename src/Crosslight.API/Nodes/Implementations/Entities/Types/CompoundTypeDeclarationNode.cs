using Crosslight.API.Nodes.Implementations.Entities.Generics;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Types
{
    /// <summary>
    /// <see cref="CompoundTypeDeclarationNode"/> abstract node represents types
    /// that can have inner declarations.
    /// E.g. in C# it is class/struct, for Java it is class,
    /// in C++ it is class/struct.
    /// </summary>
    public abstract class CompoundTypeDeclarationNode : BaseTypeDeclarationNode, IAttributesProvider, IModifiersProvider, IGenericDefinitionProvider, IIdentifierProvider
    {
        public override string Type => nameof(CompoundTypeDeclarationNode);
        public SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; protected set; }
        public SyncedList<TemplateTypeParameterConstraintClauseNode, Node> Constraints { get; protected set; }
        public int Arity { get; protected set; }
        public SyncedList<MemberDeclarationNode, Node> Members { get; protected set; }
        public CompoundTypeDeclarationNode(string identifier) : base(identifier)
        {
            TypeParameters = new SyncedList<TemplateTypeParameterNode, Node>(Children);
            Constraints = new SyncedList<TemplateTypeParameterConstraintClauseNode, Node>(Children);
            Members = new SyncedList<MemberDeclarationNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
