using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public struct GraphNodeDirection
    {
        public GraphNodeAlignment Horizontal { get; set; }
        public GraphNodeAlignment Vertical { get; set; }
        
        public static GraphNodeDirection DownRight => new GraphNodeDirection()
        {
            Horizontal = GraphNodeAlignment.Highest,
            Vertical = GraphNodeAlignment.Highest,
        };
    }
}
