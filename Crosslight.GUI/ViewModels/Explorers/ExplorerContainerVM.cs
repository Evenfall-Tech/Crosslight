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
        protected string title;
        protected string id;
        private ExplorerPanelVM top;
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }
        public string ID
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }
        public ExplorerPanelVM Top
        {
            get => top;
            set => this.RaiseAndSetIfChanged(ref top, value);
        }
        public ReactiveCommand<Unit, Unit> Close { get; }

        public RoutingState Router { get; } = new RoutingState();
        public ReactiveCommand<Func<IScreen, ExplorerPanelVM>, IRoutableViewModel> GoNext { get; }
        public ReactiveCommand<Unit, Unit> GoBack { get; }
        public ViewModelActivator Activator { get; }
        public ExplorerContainerVM()
        {
            title = "Explorer";
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
                Locator.Current.GetService<ExplorerLocator>().Close(this);
            });

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                Router.CurrentViewModel
                    .Where(x => x is ExplorerPanelVM)
                    .DistinctUntilChanged()
                    .Select(x =>
                    {
                        return Top = x as ExplorerPanelVM;
                    })
                    .Subscribe()
                    .DisposeWith(disposables);
                this.WhenAnyValue(x => x.Top)
                    .DistinctUntilChanged()
                    .Select(x =>
                    {
                        Title = x.Title;
                        ID = x.ID;
                        return Unit.Default;
                    })
                    .Subscribe()
                    .DisposeWith(disposables);
                this.WhenAnyValue(x => x.Top.ID)
                    .DistinctUntilChanged()
                    .Select(x =>
                    {
                        return ID = x;
                    })
                    .Subscribe()
                    .DisposeWith(disposables);
                this.WhenAnyValue(x => x.Top.Title)
                    .DistinctUntilChanged()
                    .Select(x =>
                    {
                        return Title = x;
                    })
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }
    }
}
