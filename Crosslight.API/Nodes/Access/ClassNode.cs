using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// ClassNode represents the class abstraction in the language.
    /// </summary>
    public class ClassNode : TypeNode
    {
        public override string Type => nameof(ClassNode);
        public ClassNode(string name) : base(name)
        {
        }
        public override string ToString()
        {
            return "ClassNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
