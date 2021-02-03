using Avalonia.Data;
using Crosslight.GUI.ViewModels.Explorers;
using Dock.Avalonia.Controls;
using Dock.Model;
using Dock.Model.Controls;
using System;
using System.Collections.Generic;

namespace Crosslight.GUI.ViewModels.Viewports
{
    public class ProjectViewportFactory : Factory
    {
        private readonly object context;

        public ProjectViewportFactory(object context)
        {
            this.context = context;
        }

        public override IDock CreateLayout()
        {
            var languagesVM = new LanguagesVM() { Id = nameof(LanguagesVM) };
            var propertiesVM = new PropertiesVM() { Id = nameof(PropertiesVM) };
            var executeVM = new ExecuteVM() { Id = nameof(ExecuteVM) };
            var resultListVM = new ResultListVM() { Id = nameof(ResultListVM) };

            var mainLayout = new ProportionalDock
            {
                Id = DockableExplorerLocator.IdUniversalDock,
                Orientation = Orientation.Horizontal,
                Proportion = double.NaN,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>
                (
                    new ProportionalDock
                    {
                        Orientation = Orientation.Vertical,
                        Proportion = 0.8,
                        ActiveDockable = null,
                        VisibleDockables = CreateList<IDockable>
                        (
                            new DocumentDock
                            {
                                Id = DockableExplorerLocator.IdResultsDock,
                                IsCollapsable = false,
                                ActiveDockable = null,
                                VisibleDockables = CreateList<IDockable>()
                            },
                            new SplitterDock(),
                            new ToolDock
                            {
                                Id = DockableExplorerLocator.IdExecuteDock,
                                ActiveDockable = executeVM,
                                Proportion = 0.2,
                                VisibleDockables = CreateList<IDockable>(executeVM)
                            }
                        )
                    },
                    new SplitterDock(),
                    new ProportionalDock
                    {
                        Orientation = Orientation.Vertical,
                        Proportion = double.NaN,
                        ActiveDockable = null,
                        VisibleDockables = CreateList<IDockable>
                        (
                            new ToolDock
                            {
                                Id =
                                    DockableExplorerLocator.IdResultListDock +
                                    " " +
                                    DockableExplorerLocator.IdLanguagesDock,
                                ActiveDockable = resultListVM,
                                VisibleDockables = CreateList<IDockable>(resultListVM, languagesVM)
                            },
                            new SplitterDock(),
                            new ToolDock
                            {
                                Id = DockableExplorerLocator.IdPropertiesDock,
                                ActiveDockable = propertiesVM,
                                VisibleDockables = CreateList<IDockable>(propertiesVM)
                            }
                        )
                    }
                )
            };

            var projectView = new ProjectViewportVM
            {
                Id = "Project",
                Title = "Project",
                ActiveDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

            var root = CreateRootDock();

            root.ActiveDockable = projectView;
            root.DefaultDockable = projectView;
            root.VisibleDockables = CreateList<IDockable>(projectView);

            return root;
        }

        public override void InitLayout(IDockable layout)
        {
            ContextLocator = new Dictionary<string, Func<object>>
            {
                [nameof(IRootDock)] = () => context,
                [nameof(IProportionalDock)] = () => context,
                [nameof(IDocumentDock)] = () => context,
                [nameof(IToolDock)] = () => context,
                [nameof(ISplitterDock)] = () => context,
                [nameof(IDockWindow)] = () => context,
                [nameof(IDocument)] = () => context,
                [nameof(ITool)] = () => context,
                [nameof(LanguagesVM)] = () => new LanguagesVM(),
                [nameof(PropertiesVM)] = () => new PropertiesVM(),
                [nameof(ExecuteVM)] = () => new ExecuteVM(),
                [nameof(ResultListVM)] = () => new ResultListVM(),
                ["Project"] = () => context,
                ["Home"] = () => context,
            };

            HostWindowLocator = new Dictionary<string, Func<IHostWindow>>
            {
                [nameof(IDockWindow)] = () =>
                {
                    var hostWindow = new HostWindow()
                    {
                        [!Avalonia.Controls.Window.TitleProperty] = new Binding("ActiveDockable.Title")
                    };
                    return hostWindow;
                }
            };

            DockableLocator = new Dictionary<string, Func<IDockable>>();

            base.InitLayout(layout);
        }
    }
}
