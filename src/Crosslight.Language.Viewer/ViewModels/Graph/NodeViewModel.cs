using Crosslight.Language.Viewer.Models.Graph;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace Crosslight.Language.Viewer.ViewModels.Graph
{
    public enum NodeState
    {
        Active,
        Inactive,
        Primary,
    }
    public class NodeViewModel : ViewModelBase, IViewModelFor<NodeModel>
    {
        public const string LeftProp   = nameof(Left);
        public const string TopProp    = nameof(Top);
        public const string WidthProp  = nameof(Width);
        public const string HeightProp = nameof(Height);

        private double left, top, width, height;
        private GraphViewModel parent;
        private NodeState state;

        public NodeViewModel(NodeModel model, GraphNodeDirection direction)
        {
            Model = model;
            Direction = direction;
        }

        public GraphNodeDirection Direction { get; }

        public NodeModel Model { get; }

        public string Data
        {
            get => Model.Data.ToString();
            set
            {
                Model.Data = value;
                this.RaisePropertyChanged(nameof(Data));
            }
        }
        public int ID
        {
            get => Model.ID;
            set
            {
                Model.ID = value;
                this.RaisePropertyChanged(nameof(ID));
            }
        }
        public string Type
        {
            get => Model.Type;
            set
            {
                Model.Type = value;
                this.RaisePropertyChanged(nameof(Type));
            }
        }
        public NodeState State
        {
            get => state;
            set => this.RaiseAndSetIfChanged(ref state, value);
        }
        public GraphViewModel Parent
        {
            get => parent;
            set => this.RaiseAndSetIfChanged(ref parent, value);
        }
        public ReactiveCommand<Unit, NodeModel> SetStartNode => ReactiveCommand.Create(
            () => Parent.StartNode = Model, 
            this.WhenAnyValue<NodeViewModel, bool, GraphViewModel>(x => x.Parent, x => x != null).DistinctUntilChanged()
        );
        // TODO: replace with observable
        public ICollection<int> Connections
        {
            get => Model.Connections;
            set
            {
                Model.Connections = value;
                this.RaisePropertyChanged(nameof(Connections));
            }
        }
        public double Left
        {
            get => left;
            set => this.RaiseAndSetIfChanged(ref left, value);
        }

        public double Top
        {
            get => top;
            set => this.RaiseAndSetIfChanged(ref top, value);
        }

        public double Width
        {
            get => width;
            set => this.RaiseAndSetIfChanged(ref width, value);
        }

        public double Height
        {
            get => height;
            set => this.RaiseAndSetIfChanged(ref height, value);
        }

        public bool IsViewModelOf(NodeModel model)
        {
            return this.Model == model;
        }
    }
}
