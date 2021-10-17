using Crosslight.API.Nodes;

namespace Crosslight.Language.Viewer.Nodes
{
    public interface IViewerVisitor : IVisitor
    {
        object Visit(ViewerNode node);
    }
}
