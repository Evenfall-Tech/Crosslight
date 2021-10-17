using Crosslight.API.Lang;
using System;

namespace Crosslight.Language.Viewer.Nodes.Visitors
{
    public class ViewerOptions : ILanguageOptions
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
