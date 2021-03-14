using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Componentization
{
    /// <summary>
    /// <see cref="ProjectNode"/> represents the project abstraction in the language.
    /// E.g. in C# it is assembly, for Java it is executable .jar,
    /// in C++ it is static program.
    /// Nothing is higher than ProjectNode, so its Parent property is null.
    /// </summary>
    public class ProjectNode : AttributedNode, INamedNode, IAttributedNode
    {
        public override string Type => nameof(ProjectNode);
        public SyncedList<ModuleNode, Node> Modules { get; protected set; }
        public string Name { get; }
        public ProjectNode(string name)
        {
            Modules = new SyncedList<ModuleNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return $"Project {Name}";
        }
        public override object AcceptVisitor(IVisitor visitor)
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
        }
    }
}
