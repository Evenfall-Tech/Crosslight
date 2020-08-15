using Crosslight.Viewer.Models.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class NodeViewModelFactory : IViewModelFactory<NodeViewModel, NodeModel>
    {
        public NodeViewModel Get(NodeModel model)
        {
            return new NodeViewModel(model);
        }
    }
}
