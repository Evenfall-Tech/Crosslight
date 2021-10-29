using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Implementations.Access.Modifiers;
using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Implementations.Entities.Fields
{
    /// <summary>
    /// <see cref="BaseFieldDeclarationNode"/> represents the field abstraction in the language.
    /// </summary>
    public abstract class BaseFieldDeclarationNode : MemberDeclarationNode, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(BaseFieldDeclarationNode);
        
        public SyncedProperty<VariableListDeclarationNode, Node> Declaration { get; protected set; }
        public BaseFieldDeclarationNode()
        {
            Declaration = new SyncedProperty<VariableListDeclarationNode, Node>(Children);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
