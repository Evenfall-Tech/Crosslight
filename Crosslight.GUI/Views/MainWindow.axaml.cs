using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Viewports;
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
        MainViewport MainViewport => this.FindControl<MainViewport>("mainViewport");
        public MainWindow()
        {
            Locator.CurrentMutable.Register(() => new MainViewport(), typeof(IViewFor<MainViewportVM>));
            Locator.CurrentMutable.Register(() => new ProjectViewport(), typeof(IViewFor<ProjectViewportVM>));

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, x => x.MainViewport, x => x.MainViewport.ViewModel)
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
