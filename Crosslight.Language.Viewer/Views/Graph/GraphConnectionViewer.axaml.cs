using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.Language.Viewer.ViewModels.Graph;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Crosslight.Language.Viewer.Views.Graph
{
    public class GraphConnectionViewer : ReactiveUserControl<ConnectionViewModel>
    {
        private Line Line => this.FindControl<Line>("line");
        public GraphConnectionViewer()
        {
            this.WhenActivated(disp =>
            {
                this.OneWayBind(this.ViewModel, x => x.FromPoint, x => x.Line.StartPoint).DisposeWith(disp);
                this.OneWayBind(this.ViewModel, x => x.ToPoint, x => x.Line.EndPoint).DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
