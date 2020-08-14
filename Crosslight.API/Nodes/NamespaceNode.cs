using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// NamespaceNode represents the namespace abstraction in the language.
    /// </summary>
    public class NamespaceNode : Node
    {
        public NamespaceNode()
        {
        }
        public override string ToString()
        {
            return "NamespaceNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
