using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Viewports;
using Crosslight.GUI.Views.Explorers;
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
        public ItemsControl ExplorerContainer => this.FindControl<ItemsControl>("explorerContainer");
        public Menu ProjectMenu => this.FindControl<Menu>("projectMenu");
        public ProjectViewport()
        {
            Locator.CurrentMutable.Register(() => new ExplorerContainer(), typeof(IViewFor<ExplorerContainerVM>));
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.MenuItems, x => x.ProjectMenu.Items)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, x => x.Containers, x => x.ExplorerContainer.Items)
                    .DisposeWith(disposables);

                this.WhenAnyValue(x => x.ViewModel)
                    .Where(x => x != null)
                    .DistinctUntilChanged()
                    .Select(y =>
                    {
                        FillProjectMenu();
                        OpenExplorer<LanguagesVM>();
                        OpenExplorer<PropertiesVM>();
                        OpenExplorer<ResultListVM>();
                        OpenExplorer<ExecuteVM>();
                        return true;
                    })
                    .Subscribe()
                    .DisposeWith(disposables);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OpenExplorer<T>() where T : ExplorerPanelVM
        {
            Locator.Current.GetService<ExplorerLocator>().Open<T>(openExisting: true, createNewExplorer: true);
        }

        private void FillProjectMenu()
        {
            var openView = ReactiveCommand.Create<Type>(t =>
            {
                Locator.Current.GetService<ExplorerLocator>().Open(t, openExisting: true);
            });
            this.ViewModel.MenuItems.AddRange(new[]
            {
                    new MenuItemVM { Header = "_File" },
                    new MenuItemVM { Header = "_Edit" },
                    new MenuItemVM
                    {
                        Header = "_View",
                        Items = new ObservableCollectionExtended<MenuItemVM>()
                        {
                            new MenuItemVM
                            {
                                Header = $"_{LanguagesVM.ConstTitle}",
                                Command = openView,
                                CommandParameter = typeof(LanguagesVM),
                            },
                            new MenuItemVM
                            {
                                Header = $"_{PropertiesVM.ConstTitle}",
                                Command = openView,
                                CommandParameter = typeof(PropertiesVM),
                            },
                            new MenuItemVM
                            {
                                Header = $"{ExecuteVM.ConstTitle}",
                                Command = openView,
                                CommandParameter = typeof(ExecuteVM),
                            },
                            new MenuItemVM
                            {
                                Header = $"{ResultListVM.ConstTitle}",
                                Command = openView,
                                CommandParameter = typeof(ResultListVM),
                            },
                            new MenuItemVM
                            {
                                Header = $"{SourcePreviewVM.ConstTitle}",
                                Command = openView,
                                CommandParameter = typeof(SourcePreviewVM),
                            },
                        },
                    },
                    new MenuItemVM { Header = "_Run" },
                });
        }
    }
}
