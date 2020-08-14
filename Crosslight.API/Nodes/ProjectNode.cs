using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ProjectNode represents the project abstraction in the language.
    /// E.g. in C# it is assembly, for Java it is executable .jar,
    /// in C++ it is static program.
    /// Nothing is higher than ProjectNode, so its Parent property is null.
    /// </summary>
    public class ProjectNode : Node
    {
        public ProjectNode()
        {
        }
        public override string ToString()
        {
            return "ProjectNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
