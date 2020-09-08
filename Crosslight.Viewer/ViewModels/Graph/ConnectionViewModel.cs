using Avalonia;
using System;
using System.Linq;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class ConnectionViewModel : ViewModelBase
    {
        private const string FromProp = nameof(From);
        private const string FromXProp = nameof(FromX);
        private const string FromYProp = nameof(FromY);
        private const string FromPointProp = nameof(FromPoint);
        private const string ToProp = nameof(To);
        private const string ToXProp = nameof(ToX);
        private const string ToYProp = nameof(ToY);
        private const string ToPointProp = nameof(ToPoint);
        private NodeViewModel from, to;

        public ConnectionViewModel(NodeViewModel from, NodeViewModel to)
        {
            From = from;
            To = to;
        }

        public NodeViewModel From
        {
            get => from;
            set
            {
                if (from != null)
                {
                    from.PropertyChanged -= From_PropertyChanged;
                }
                from = value;
                if (from != null)
                {
                    from.PropertyChanged += From_PropertyChanged;
                }
                OnPropertyChanged(FromProp);
                OnPropertyChanged(FromXProp);
                OnPropertyChanged(FromYProp);
                OnPropertyChanged(FromPointProp);
            }
        }

        public NodeViewModel To
        {
            get => to;
            set
            {
                if (to != null)
                {
                    to.PropertyChanged -= To_PropertyChanged;
                }
                to = value;
                if (to != null)
                {
                    to.PropertyChanged += To_PropertyChanged;
                }
                OnPropertyChanged(ToProp);
                OnPropertyChanged(ToXProp);
                OnPropertyChanged(ToYProp);
                OnPropertyChanged(ToPointProp);
            }
        }

        public double FromX
        {
            get => from.Left + from.Width / 2.0;
        }

        public double FromY
        {
            get => from.Top + from.Height / 2.0;
        }

        public Point FromPoint
        {
            get => new Point(FromX, FromY);
        }

        public double ToX
        {
            get => to.Left + to.Width / 2.0;
        }

        public double ToY
        {
            get => to.Top + to.Height / 2.0;
        }

        public Point ToPoint
        {
            get => new Point(ToX, ToY);
        }

        private static readonly string[] nodeProperties = new string[]
        {
            NodeViewModel.LeftProp, NodeViewModel.TopProp, NodeViewModel.WidthProp, NodeViewModel.HeightProp
        };

        private void From_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (nodeProperties.Contains(e.PropertyName))
            {
                OnPropertyChanged(FromXProp);
                OnPropertyChanged(FromYProp);
                OnPropertyChanged(FromPointProp);
            }
        }

        private void To_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (nodeProperties.Contains(e.PropertyName))
            {
                OnPropertyChanged(ToXProp);
                OnPropertyChanged(ToYProp);
                OnPropertyChanged(ToPointProp);
            }
        }
    }
}
