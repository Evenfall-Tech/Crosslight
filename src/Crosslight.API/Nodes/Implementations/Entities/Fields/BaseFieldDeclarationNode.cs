using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Fields
{
    /// <summary>
    /// <see cref="BaseFieldDeclarationNode"/> represents the field abstraction in the language.
    /// </summary>
    public abstract class BaseFieldDeclarationNode : MemberDeclarationNode, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(BaseFieldDeclarationNode);

        private readonly SyncedProperty<VariableListDeclarationNode, Node> declaration;
        public VariableListDeclarationNode Declaration
        {
            get => declaration.Value;
            set => declaration.Value = value;
        }
        public BaseFieldDeclarationNode()
        {
            declaration = new SyncedProperty<VariableListDeclarationNode, Node>(Children);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
