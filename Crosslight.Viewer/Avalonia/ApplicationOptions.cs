using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes.Visitors;

namespace Crosslight.Viewer.Avalonia
{
    public class ApplicationOptions
    {
        public ViewerOptions Options { get; set; }
        public Node RootNode { get; set; }
    }
}
