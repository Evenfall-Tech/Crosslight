using Crosslight.API.Lang;
using Crosslight.API.Nodes;

namespace Crosslight.Viewer.Avalonia
{
    public class ApplicationOptions
    {
        public LanguageOptions Options { get; set; }
        public Node RootNode { get; set; }
    }
}
