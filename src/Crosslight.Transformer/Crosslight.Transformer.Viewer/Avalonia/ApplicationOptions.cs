using Crosslight.API.Nodes.Implementations;
using Crosslight.Transformer.Viewer.Nodes.Visitors;

namespace Crosslight.Transformer.Viewer.Avalonia
{
    public class ApplicationOptions
    {
        public ViewerOptions Options { get; set; }
        public Node RootNode { get; set; }
    }
}
