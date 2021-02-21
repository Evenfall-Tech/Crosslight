using Crosslight.API.Nodes.Entities;
using Crosslight.API.Nodes.Interfaces;
using System;

namespace Crosslight.API.Nodes.Function
{
    /// <summary>
    /// <see cref="BaseMethodNode"/> represents the method abstraction in the language.
    /// </summary>
    public abstract class BaseMethodNode : BaseFunctionNode, ITypeMemberNode, INamedNode, IModifiedNode
    {
        public override string Type => nameof(BaseMethodNode);
        public FunctionalTypeNode OwningType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public BaseMethodNode(string name) : base(name)
        {
        }
    }
}
