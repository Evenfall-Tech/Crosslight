using Crosslight.API.Util;
using System;

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
        public override Type Type => typeof(ProjectNode);
        public SyncedList<ModuleNode, Node> Modules { get; protected set; }
        public string Name { get; }
        public ProjectNode(string name)
        {
            Modules = new SyncedList<ModuleNode, Node>(Children);
            Name = name;
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
