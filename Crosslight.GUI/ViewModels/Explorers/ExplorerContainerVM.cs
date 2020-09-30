using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExplorerContainerVM : ReactiveObject, IActivatableViewModel, IScreen
    {
        protected string title;
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        public RoutingState Router { get; } = new RoutingState();
        public ReactiveCommand<Func<IScreen, ExplorerPanelVM>, IRoutableViewModel> GoNext { get; }
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;
        public ViewModelActivator Activator { get; }
        public ExplorerContainerVM()
        {
            title = "Explorer";
            GoNext = ReactiveCommand.CreateFromObservable(
                (Func<IScreen, ExplorerPanelVM> x) => Router.Navigate.Execute(x(this))
            );

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                Router.CurrentViewModel
                    .Where(x => x is ExplorerPanelVM)
                    .Subscribe(x => Title = (x as ExplorerPanelVM).Title)
                    .DisposeWith(disposables);
            });
        }
    }
}
