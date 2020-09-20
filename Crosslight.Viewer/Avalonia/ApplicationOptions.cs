using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes.Visitors;

namespace Crosslight.Viewer.Avalonia
{
    public class ApplicationOptions
    {
        public VisitOptions Options { get; set; }
        public Node RootNode { get; set; }
    }
}
