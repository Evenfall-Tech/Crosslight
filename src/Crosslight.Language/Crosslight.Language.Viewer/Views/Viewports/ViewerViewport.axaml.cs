using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.Language.Viewer.ViewModels.Viewports;
using Crosslight.Language.Viewer.Views.Graph;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Crosslight.Language.Viewer.Views.Viewports
{
    public class ViewerViewport : ReactiveUserControl<ViewerViewportViewModel>
    {
        public GraphViewer GraphViewer => this.FindControl<GraphViewer>("graphViewer");
        public ViewerViewport()
        {
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(this.ViewModel, x => x.GraphViewModel, x => x.GraphViewer.ViewModel)
                    .DisposeWith(disposables);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
