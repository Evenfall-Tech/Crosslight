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
        public Button CloseButton => this.FindControl<Button>("close");
        public RoutedViewHost RoutedView => this.FindControl<RoutedViewHost>("routedView");
        public ExplorerContainer()
        {
            Locator.CurrentMutable.Register(() => new Languages(), typeof(IViewFor<LanguagesVM>));
            Locator.CurrentMutable.Register(() => new Properties(), typeof(IViewFor<PropertiesVM>));
            Locator.CurrentMutable.Register(() => new SourcePreview(), typeof(IViewFor<SourcePreviewVM>));
            Locator.CurrentMutable.Register(() => new Execute(), typeof(IViewFor<ExecuteVM>));
            Locator.CurrentMutable.Register(() => new Results(), typeof(IViewFor<ResultsVM>));
            Locator.CurrentMutable.Register(() => new ResultList(), typeof(IViewFor<ResultListVM>));
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.Title, x => x.Title.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.Router, x => x.RoutedView.Router)
                    .DisposeWith(disposables);
                this.WhenAnyValue(x => x.ViewModel.Close)
                    .BindTo(this, x => x.CloseButton.Command)
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
