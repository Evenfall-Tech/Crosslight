using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Componentization
{
    /// <summary>
    /// <see cref="ProjectNode"/> represents the project abstraction in the language.
    /// E.g. in C# it is assembly, for Java it is executable .jar,
    /// in C++ it is static program.
    /// Nothing is higher than ProjectNode, so its Parent property is null.
    /// </summary>
    public class ProjectNode : Node, IIdentifierProvider, IAttributesProvider
    {
        public override string Type => nameof(ProjectNode);
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<ModuleNode, Node> Modules { get; protected set; }
        public string Identifier { get; }
        public ProjectNode(string identifier)
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Modules = new SyncedList<ModuleNode, Node>(Children);
            Identifier = identifier;
        }
        public override string ToString()
        {
            return $"Project {Identifier}";
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
