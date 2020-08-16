using Crosslight.API.Nodes;

namespace Crosslight.Viewer.Nodes
{
    public interface IViewerVisitor : IVisitor
    {
        object Visit(ViewerNode node);
    }
}
