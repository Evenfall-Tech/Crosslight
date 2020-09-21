using Crosslight.Viewer.Models.Graph;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class NodeViewModelFactory : IViewModelFactory<NodeViewModel, NodeModel>
    {
        private readonly GraphViewModel graphViewModel;
        private readonly GraphNodeDirection direction;
        public NodeViewModelFactory(GraphViewModel graphViewModel, GraphNodeDirection direction)
        {
            this.graphViewModel = graphViewModel;
            this.direction = direction;
        }
        public NodeViewModel Get(NodeModel model)
        {
            return Get(model, true);
        }
        public NodeViewModel Get(NodeModel model, bool active)
        {
            return new NodeViewModel(model, direction, active)
            {
                Parent = graphViewModel,
            };
        }
    }
}
