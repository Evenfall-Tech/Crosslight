using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ClassNode represents the class abstraction in the language.
    /// </summary>
    public class ClassNode : TypeNode
    {
        public override Type Type => typeof(ClassNode);
        public ClassNode()
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
