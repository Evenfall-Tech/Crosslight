using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// VariableNode represents the variable abstraction in the language.
    /// </summary>
    public class VariableNode : ValueNode
    {
        public override string Type => nameof(VariableNode);
        public string Name { get; }
        public VariableNode(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return "VariableNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
