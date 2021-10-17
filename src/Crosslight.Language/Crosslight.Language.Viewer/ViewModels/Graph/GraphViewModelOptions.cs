using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Componentization;
using Crosslight.API.Nodes.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Language.Viewer.ViewModels.Graph
{
    public class GraphViewModelOptions
    {
        public const int DefaultVisibilityChild = 3, DefauldVisibilityParent = 1;
        private const int MaxVisibilityChild = 20, MaxVisibilityParent = 4;
        private const int MaxNaviationStackSize = 20;

        public static Dictionary<string, (int, int)> VisibilityRange { get; } = new Dictionary<string, (int, int)>()
        {
            { nameof(RootNode), (DefauldVisibilityParent, 4) },
            { nameof(DummyNode), (MaxVisibilityParent, MaxVisibilityChild) },
            { nameof(MetadataNode), (MaxVisibilityParent, MaxVisibilityChild) },
            { nameof(ProjectNode), (MaxVisibilityParent, 3) }, // Module, Namespace, Entity
            { nameof(ModuleNode), (MaxVisibilityParent, 2) }, // Namespace, Entity
            { nameof(NamespaceNode), (MaxVisibilityParent, 2) }, // Entity, Base Children
        };
        public static int NavigationStackSize { get; } = MaxNaviationStackSize;

        public GraphNodeAlignment HorizontalAlignment { get; set; }
        public GraphNodeAlignment VerticalAlignment { get; set; }
        public GraphNodeDirection NodeDirection { get; set; }
    }
}
