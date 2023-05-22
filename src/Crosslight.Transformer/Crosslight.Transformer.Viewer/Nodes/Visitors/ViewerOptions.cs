using Crosslight.API.Transformers;

namespace Crosslight.Transformer.Viewer.Nodes.Visitors
{
    public class ViewerOptions : ITransformerOptions
    {
        public bool LaunchApplication { get; set; }
        public bool UseIconsForNodes { get; set; }

        public ViewerOptions()
        {
            // TODO: fill this class with options.
            LaunchApplication = false;
            UseIconsForNodes = true;
        }

        public ViewerOptions(ViewerOptions other)
        {
            LaunchApplication = other.LaunchApplication;
            UseIconsForNodes = other.UseIconsForNodes;
        }

        public object Clone()
        {
            return new ViewerOptions(this);
        }
    }
}
