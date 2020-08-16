using Avalonia.Controls;
using Crosslight.Viewer.Models.Graph;
using Crosslight.Viewer.Views.Utils;
using System.Collections.Generic;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class NodeViewModel : ViewModelBase, IViewModelFor<NodeModel>
    {
        private NodeModel model;
        private double left, top, width, height;

        public NodeViewModel(NodeModel model)
        {
            this.model = model;
            UpdateSize(model?.Data);
        }

        public void UpdateSize(object data)
        {
            if (data != null)
            {
                if (data is ControlWrapper wrapper)
                {
                    width = wrapper.Width;
                    height = wrapper.Height;
                    if (wrapper.Tag is GraphNodeDirection dir)
                    {
                        Direction = dir;
                    }
                    else
                    {
                        Direction = GraphNodeDirection.DownRight;
                    }
                }
                else if (data is Control control)
                {
                    var size = SizeMeasures.GetMinControlSize(control);
                    Width = size.Width;
                    Height = size.Height;
                    Direction = GraphNodeDirection.DownRight;
                }
                else
                {
                    width = 1.0;
                    height = 1.0;
                    Direction = GraphNodeDirection.DownRight;
                }
            }
            else
            {
                width = 0.0;
                height = 0.0;
                Direction = GraphNodeDirection.DownRight;
            }
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

        public GraphNodeDirection Direction { get; protected set; }

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
