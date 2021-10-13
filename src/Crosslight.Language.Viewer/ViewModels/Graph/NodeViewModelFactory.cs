using Crosslight.Language.Viewer.Models.Graph;

namespace Crosslight.Language.Viewer.ViewModels.Graph
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
            return Get(model, NodeState.Active);
        }
        public NodeViewModel Get(NodeModel model, NodeState state)
        {
            return new NodeViewModel(model, direction)
            {
                Parent = graphViewModel,
                State = state,
            };
        }
    }
}
