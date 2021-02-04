using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Viewports;
using Crosslight.GUI.Views.Explorers;
using Dock.Avalonia.Controls;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

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
