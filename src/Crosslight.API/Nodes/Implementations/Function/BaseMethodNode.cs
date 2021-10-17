using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using System;

namespace Crosslight.API.Nodes.Implementations.Function
{
    /// <summary>
    /// <see cref="BaseMethodNode"/> represents the method abstraction in the language.
    /// </summary>
    public abstract class BaseMethodNode : BaseFunctionNode, /*ITypeMemberNode, */IIdentifierProvider, IModifiersProvider, IAttributesProvider
    {
        public override string Type => nameof(BaseMethodNode);
        // TODO: consider if this should be added
        // public FunctionalTypeDeclarationNode OwningType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public BaseMethodNode(string identifier) : base(identifier)
        {
        }
    }
}
