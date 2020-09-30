using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class ExplorerContainer : ReactiveUserControl<ExplorerContainerVM>
    {
        public TextBlock Title => this.FindControl<TextBlock>("title");
        public RoutedViewHost RoutedView => this.FindControl<RoutedViewHost>("routedView");
        public ExplorerContainer()
        {
            Locator.CurrentMutable.Register(() => new Languages(), typeof(IViewFor<LanguagesVM>));
            Locator.CurrentMutable.Register(() => new Properties(), typeof(IViewFor<PropertiesVM>));
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.Title, x => x.Title.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.Router, x => x.RoutedView.Router)
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
