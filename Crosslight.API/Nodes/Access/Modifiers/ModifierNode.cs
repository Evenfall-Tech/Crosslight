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
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
