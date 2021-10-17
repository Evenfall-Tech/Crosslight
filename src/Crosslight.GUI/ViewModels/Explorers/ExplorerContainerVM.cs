using ReactiveUI;
using Splat;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExplorerContainerVM : ReactiveObject, IActivatableViewModel, IScreen
    {
        protected readonly ObservableAsPropertyHelper<string> title;
        protected readonly ObservableAsPropertyHelper<string> id;
        protected readonly ObservableAsPropertyHelper<ExplorerPanelVM> top;
        public string Title => title.Value;
        public string ID => id.Value;
        public ExplorerPanelVM Top => top.Value;
        public ReactiveCommand<Unit, Unit> Close { get; }

        public RoutingState Router { get; } = new RoutingState();
        public ReactiveCommand<Func<IScreen, ExplorerPanelVM>, IRoutableViewModel> GoNext { get; }
        public ReactiveCommand<Unit, Unit> GoBack { get; }
        public ViewModelActivator Activator { get; }
        public ExplorerContainerVM()
        {
            GoNext = ReactiveCommand.CreateFromObservable(
                (Func<IScreen, ExplorerPanelVM> x) =>
                {
                    //Top = x(this);
                    return Router.Navigate.Execute(x(this));
                }
            );
            GoBack = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    //if (Router.NavigationStack.Count > 1)
                    //    Top = Router.NavigationStack[Router.NavigationStack.Count - 2] as ExplorerPanelVM;
                    return Router.NavigateBack;
                }
            );
            Close = ReactiveCommand.Create(() =>
            {
                Locator.Current.GetService<IExplorerLocator>().Close(Top);
            });

            top = Router.CurrentViewModel
                .Where(x => x is ExplorerPanelVM)
                .DistinctUntilChanged()
                .Select(x => x as ExplorerPanelVM)
                .ToProperty(this, x => x.Top);
            id = this
                .WhenAnyValue(x => x.Top, x => x.Top.Id, (top, id) => top.Id ?? id)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .DistinctUntilChanged()
                .ToProperty(this, x => x.ID);
            title = this
                .WhenAnyValue(x => x.Top, x => x.Top.Title, (top, title) => top.Title ?? title)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .DistinctUntilChanged()
                .ToProperty(this, x => x.Title, "Explorer");

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
