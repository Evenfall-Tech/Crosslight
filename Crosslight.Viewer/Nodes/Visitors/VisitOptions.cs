using System;

namespace Crosslight.Viewer.Nodes.Visitors
{
    public class VisitOptions : ICloneable
    {
        public bool LaunchApplication { get; set; }
        public bool UseIconsForNodes { get; set; }

        public VisitOptions()
        {
            // TODO: fill this class with options.
            LaunchApplication = false;
            UseIconsForNodes = true;
        }

        public VisitOptions(VisitOptions other)
        {
            LaunchApplication = other.LaunchApplication;
            UseIconsForNodes = other.UseIconsForNodes;
        }

        public object Clone()
        {
            return new VisitOptions(this);
        }
    }
}
