using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.Avalonia
{
    public class ApplicationOptions
    {
        public LanguageOptions Options { get; set; }
        public Node RootNode { get; set; }
    }
}
