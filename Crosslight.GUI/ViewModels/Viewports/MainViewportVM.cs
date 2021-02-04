using Dock.Model;
using Dock.Model.Controls;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using System.Reactive;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels.Viewports
{
    public class MainViewportVM : BaseViewModel, IActivatableViewModel
    {
        protected ObservableCollectionExtended<MenuItemVM> menuItems;
        protected IFactory factory;
        protected IDock layout;

        public ObservableCollectionExtended<MenuItemVM> MenuItems
        {
            get => menuItems;
            set => this.RaiseAndSetIfChanged(ref menuItems, value);
        }

        public IFactory Factory
        {
            get => factory;
            set => this.RaiseAndSetIfChanged(ref factory, value);
        }

        public IDock Layout
        {
            get => layout;
            set => this.RaiseAndSetIfChanged(ref layout, value);
        }

        public ReactiveCommand<Unit, Unit> ResetView => ReactiveCommand.Create(ResetLayout);

        public ViewModelActivator Activator { get; }
        public MainViewportVM()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new DockableExplorerLocator(this), typeof(IExplorerLocator));

            menuItems = new ObservableCollectionExtended<MenuItemVM>();

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }

        private void ResetLayout()
        {
            if (Layout != null)
            {
                Layout.Close();
            }

            var layout = Factory?.CreateLayout();
            if (layout != null)
            {
                Layout = layout as IRootDock;
                Factory?.InitLayout(layout);
            }
        }
    }
}
