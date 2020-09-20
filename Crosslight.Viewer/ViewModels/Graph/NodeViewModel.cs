using Crosslight.Viewer.Models.Graph;
using ReactiveUI;
using System.Collections.Generic;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class NodeViewModel : ViewModelBase, IViewModelFor<NodeModel>
    {
        public const string LeftProp   = nameof(Left);
        public const string TopProp    = nameof(Top);
        public const string WidthProp  = nameof(Width);
        public const string HeightProp = nameof(Height);

        private double left, top, width, height;

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
