using Crosslight.Transformer.Viewer.Models.Graph;
using ReactiveUI;

namespace Crosslight.Transformer.Viewer.ViewModels.Graph
{
    public class GraphRoutableViewModel : ViewModelBase, IRoutableViewModel
    {
        private NodeModel node;

        public IScreen HostScreen { get; }

        public NodeModel Node
        {
            get => node;
            set => this.RaiseAndSetIfChanged(ref node, value);
        }
        // TODO: replace on collision.
        public string UrlPathSegment => $"Node{node.ID}";

        public GraphRoutableViewModel(IScreen screen) => HostScreen = screen;
        public GraphRoutableViewModel(NodeModel model, IScreen screen) : this(screen)
        {
            node = model;
        }
    }
}
