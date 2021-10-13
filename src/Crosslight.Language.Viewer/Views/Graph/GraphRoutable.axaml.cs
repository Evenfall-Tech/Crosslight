using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.Language.Viewer.ViewModels.Graph;
using ReactiveUI;

namespace Crosslight.Language.Viewer.Views.Graph
{
    public class GraphRoutable : ReactiveUserControl<GraphRoutableViewModel>
    {
        public GraphRoutable()
        {
            this.WhenActivated(disposables => { });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
