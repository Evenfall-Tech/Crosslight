using Crosslight.Viewer.Models.Graph;
using System.Collections.Generic;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class NodeViewModel : ViewModelBase, IViewModelFor<NodeModel>
    {
        public const string ModelProp = "Model";
        public const string DataProp = "Data";
        public const string IDProp = "ID";
        public const string ConnectionsProp = "Connections";
        public const string LeftProp = "Left";
        public const string TopProp = "Top";
        public const string WidthProp = "Width";
        public const string HeightProp = "Height";

        private NodeModel model;
        private double left, top, width, height;

        public NodeViewModel(NodeModel model, GraphNodeDirection direction)
        {
            this.model = model;
            Direction = direction;
        }

        public NodeModel Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged(ModelProp);
            }
        }

        public object Data
        {
            get => model.Data;
            set
            {
                model.Data = value;
                OnPropertyChanged(DataProp);
            }
        }

        public int ID
        {
            get => model.ID;
            set
            {
                model.ID = value;
                OnPropertyChanged(IDProp);
            }
        }

        //TODO: rewrite this with an observable collection
        public ICollection<int> Connections
        {
            get => model.Connections;
            set
            {
                model.Connections = value;
                OnPropertyChanged(ConnectionsProp);
            }
        }

        public GraphNodeDirection Direction { get; }

        public double Left
        {
            get => left;
            set
            {
                left = value;
                OnPropertyChanged(LeftProp);
            }
        }

        public double Top
        {
            get => top;
            set
            {
                top = value;
                OnPropertyChanged(TopProp);
            }
        }

        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged(WidthProp);
            }
        }

        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(HeightProp);
            }
        }

        public bool IsViewModelOf(NodeModel model)
        {
            return this.model == model;
        }
    }
}
