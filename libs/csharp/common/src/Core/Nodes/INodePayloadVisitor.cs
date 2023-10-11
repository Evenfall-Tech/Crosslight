using Crosslight.src.Core.Nodes;

namespace Crosslight.Core.Nodes
{
    public interface INodePayloadVisitor
    {
        void VisitSourceRoot(SourceRoot payload);
        void VisitScope(Scope payload);
    }
}
