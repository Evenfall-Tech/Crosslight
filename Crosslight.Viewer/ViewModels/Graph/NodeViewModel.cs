using Crosslight.Viewer.Models.Graph;
using System.Collections.Generic;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class NodeViewModel : ViewModelBase, IViewModelFor<NodeModel>
    {
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
                OnPropertyChanged("Model");
            }
        }

        public object Data
        {
            get => model.Data;
            set
            {
                model.Data = value;
                OnPropertyChanged("Data");
            }
        }

        public int ID
        {
            get => model.ID;
            set
            {
                model.ID = value;
                OnPropertyChanged("ID");
            }
        }

        //TODO: rewrite this with an observable collection
        public ICollection<int> Connections
        {
            get => model.Connections;
            set
            {
                model.Connections = value;
                OnPropertyChanged("Connections");
            }
        }

        public GraphNodeDirection Direction { get; }

        public double Left
        {
            get => left;
            set
            {
                left = value;
                OnPropertyChanged("Left");
            }
        }

        public double Top
        {
            get => top;
            set
            {
                top = value;
                OnPropertyChanged("Top");
            }
        }

        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public bool IsViewModelOf(NodeModel model)
        {
            return this.model == model;
        }
    }
}
