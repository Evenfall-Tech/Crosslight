using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;

namespace Crosslight.API.Nodes.Implementations.Access
{
    /// <summary>
    /// <see cref="VariableNode"/> represents the variable abstraction in the language.
    /// </summary>
    public class VariableNode : ValueNode, IAttributesProvider, IModifiersProvider, IIdentifierProvider
    {
        public override string Type => nameof(VariableNode);
        public string Identifier { get; }
        public VariableNode(string identifier)
        {
            Identifier = identifier;
        }
        public override string ToString()
        {
            return "VariableNode";
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
    }
}
