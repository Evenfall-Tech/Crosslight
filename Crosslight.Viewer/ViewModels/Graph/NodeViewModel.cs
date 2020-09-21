using Crosslight.Viewer.Models.Graph;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class NodeViewModel : ViewModelBase, IViewModelFor<NodeModel>
    {
        public const string LeftProp   = nameof(Left);
        public const string TopProp    = nameof(Top);
        public const string WidthProp  = nameof(Width);
        public const string HeightProp = nameof(Height);

        private double left, top, width, height;
        private bool active;
        private GraphViewModel parent;

        public NodeViewModel(NodeModel model, GraphNodeDirection direction, bool active = true)
        {
            Model = model;
            Direction = direction;
            this.active = active;
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
        public bool Active
        {
            get => active;
            set => this.RaiseAndSetIfChanged(ref active, value);
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
