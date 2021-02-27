using System;

namespace Crosslight.API.Nodes.Access.Modifiers
{
    /// <summary>
    /// <see cref="ModifierNode"/> represents the modifier (public, abstract, virtual).
    /// </summary>
    public class ModifierNode : Node
    {
        public override string Type => nameof(ModifierNode);
        public ModifierToken Token { get; protected set; }
        public ModifierGroup Group { get; protected set; }
        public ModifierNode(ModifierToken token, ModifierGroup group)
        {
            Token = token;
        }
        public override string ToString()
        {
            return "ModifierNode";
        }
        // TODO: fix this.
        /*public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }*/

        public static ModifierGroup GetModifierGroup(ModifierToken token)
        {
            switch (token)
            {
                case ModifierToken.None:
                    return ModifierGroup.None;
                case ModifierToken.Custom:
                    return ModifierGroup.Custom;
                case ModifierToken.Internal:
                case ModifierToken.Public:
                case ModifierToken.Protected:
                case ModifierToken.Private:
                case ModifierToken.Readonly:
                    return ModifierGroup.Access;
                case ModifierToken.Abstract:
                case ModifierToken.Virtual:
                case ModifierToken.Sealed:
                case ModifierToken.New:
                case ModifierToken.Override:
                case ModifierToken.Static:
                    return ModifierGroup.InheritanceControl;
                case ModifierToken.Implicit:
                case ModifierToken.Explicit:
                    return ModifierGroup.ConversionType;
                case ModifierToken.Async:
                    return ModifierGroup.Parallelism;
                case ModifierToken.Volatile:
                case ModifierToken.Unsafe:
                case ModifierToken.Extern:
                    return ModifierGroup.Optimizations;
                case ModifierToken.In:
                case ModifierToken.Out:
                case ModifierToken.Ref:
                case ModifierToken.Params:
                    return ModifierGroup.ParameterPassing;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
