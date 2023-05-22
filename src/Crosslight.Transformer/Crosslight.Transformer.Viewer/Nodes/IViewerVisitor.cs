using Crosslight.API.Nodes;

namespace Crosslight.Transformer.Viewer.Nodes
{
    public interface IViewerVisitor : IVisitor
    {
        object Visit(ViewerNode node);
    }
}
