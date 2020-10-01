using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.Views.Explorers;
using Crosslight.GUI.Views.Viewports;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Disposables;

namespace Crosslight.GUI.Views
{
    public class MainWindow : ReactiveWindow<MainWindowVM>
    {
        ProjectViewport ProjectViewport => this.FindControl<ProjectViewport>("projectViewport");
        public MainWindow()
        {
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.Project, x => x.ProjectViewport.ViewModel)
                    .DisposeWith(disposables);
            });
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
