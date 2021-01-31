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
            var languagesVM = new LanguagesVM() { Id = nameof(LanguagesVM), Title = nameof(LanguagesVM) };
            var propertiesVM = new PropertiesVM() { Id = nameof(PropertiesVM), Title = nameof(PropertiesVM) };
            var executeVM = new ExecuteVM() { Id = nameof(ExecuteVM), Title = nameof(ExecuteVM) };
            var resultListVM = new ResultListVM() { Id = nameof(ResultListVM), Title = nameof(ResultListVM) };

            var mainLayout = new ProportionalDock
            {
                Orientation = Orientation.Horizontal,
                VisibleDockables = CreateList<IDockable>
                (
                    new ProportionalDock
                    {
                        Orientation = Orientation.Vertical,
                        ActiveDockable = null,
                        VisibleDockables = CreateList<IDockable>
                        (
                            new DocumentDock
                            {
                                IsCollapsable = false,
                                ActiveDockable = null,
                                VisibleDockables = CreateList<IDockable>()
                            },
                            new SplitterDock(),
                            new ToolDock
                            {
                                ActiveDockable = executeVM,
                                VisibleDockables = CreateList<IDockable>(executeVM)
                            }
                        )
                    },
                    new SplitterDock(),
                    new ProportionalDock
                    {
                        Orientation = Orientation.Vertical,
                        ActiveDockable = null,
                        VisibleDockables = CreateList<IDockable>
                        (
                            new ToolDock
                            {
                                ActiveDockable = resultListVM,
                                VisibleDockables = CreateList<IDockable>(resultListVM, languagesVM)
                            },
                            new SplitterDock(),
                            new ToolDock
                            {
                                ActiveDockable = propertiesVM,
                                VisibleDockables = CreateList<IDockable>(propertiesVM)
                            }
                        )
                    }
                )
            };

            var projectView = new ProjectViewportVM
            {
                Id = "Project1",
                Title = "Project1",
                ActiveDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

            var root = CreateRootDock();

            root.IsCollapsable = true;
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
                ["Dashboard"] = () => layout,
                ["Project1"] = () => context,
                ["Home"] = () => context,
            };

            HostWindowLocator = new Dictionary<string, Func<IHostWindow>>
            {
                [nameof(IDockWindow)] = () => new HostWindow()
            };

            DockableLocator = new Dictionary<string, Func<IDockable>>();

            base.InitLayout(layout);
        }
    }
}
