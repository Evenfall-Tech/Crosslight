using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    /// <summary>
    /// <see cref="BaseTypeDeclarationNode"/> abstract node represents a type
    /// that has one or more base types.
    /// Similar to Base Type Declaration Syntax.
    /// </summary>
    public abstract class BaseTypeDeclarationNode : MemberDeclarationNode, IAttributesProvider, IModifiersProvider, IIdentifierProvider
    {
        public override string Type => nameof(BaseTypeDeclarationNode);
        public string Identifier { get; }
        public SyncedList<BaseTypeDeclarationNode, Node> BaseTypes { get; protected set; }
        public BaseTypeDeclarationNode(string identifier)
        {
            BaseTypes = new SyncedList<BaseTypeDeclarationNode, Node>(Children);
            Identifier = identifier;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
