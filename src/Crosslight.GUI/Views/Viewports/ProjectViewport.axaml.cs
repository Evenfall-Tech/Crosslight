using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Viewports;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Crosslight.GUI.Views.Viewports
{
    public class ProjectViewport : ReactiveUserControl<ProjectViewportVM>
    {
        public ContentControl ContentControl => this.FindControl<ContentControl>("content");
        public ProjectViewport()
        {
            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, x => x.ActiveDockable, x => x.ContentControl.Content)
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
