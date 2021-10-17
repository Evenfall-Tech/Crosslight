﻿using Crosslight.API.Nodes.Implementations;
using Crosslight.Language.Viewer.Nodes.Visitors;

namespace Crosslight.Language.Viewer.Avalonia
{
    public class ApplicationOptions
    {
        public ViewerOptions Options { get; set; }
        public Node RootNode { get; set; }
    }
}
