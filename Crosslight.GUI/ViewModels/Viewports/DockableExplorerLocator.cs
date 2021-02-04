using Crosslight.GUI.ViewModels.Explorers;
using Dock.Model;
using Dock.Model.Controls;
using System;
using System.Collections.Generic;

namespace Crosslight.GUI.ViewModels.Viewports
{
    public class DockableExplorerLocator : IExplorerLocator
    {
        public const string IdUniversalDock = "UniversalDock";
        public const string IdResultsDock = nameof(ResultsVM) + "Dock";
        public const string IdResultListDock = nameof(ResultListVM) + "Dock";
        public const string IdExecuteDock = nameof(ExecuteVM) + "Dock";
        public const string IdPropertiesDock = nameof(PropertiesVM) + "Dock";
        public const string IdLanguagesDock = nameof(LanguagesVM) + "Dock";

        private readonly MainViewportVM main;
        private readonly Dictionary<Type, (Func<ExplorerPanelVM> func, bool singleton, string parentId)> factory;
        private readonly Dictionary<Type, ExplorerPanelVM> singletons;

        public DockableExplorerLocator(MainViewportVM mainViewport)
        {
            main = mainViewport;
            factory = new Dictionary<Type, (Func<ExplorerPanelVM> func, bool singleton, string parentId)>()
            {
                { typeof(LanguagesVM), (() => new LanguagesVM() { Id = nameof(LanguagesVM) }, true, IdLanguagesDock) },
                { typeof(PropertiesVM), (() => new PropertiesVM() { Id = nameof(PropertiesVM) }, true, IdPropertiesDock) },
                { typeof(ExecuteVM), (() => new ExecuteVM() { Id = nameof(ExecuteVM) }, true, IdExecuteDock) },
                { typeof(ResultListVM), (() => new ResultListVM() { Id = nameof(ResultListVM) }, true, IdResultListDock) },
                { typeof(ResultsVM), (() => new ResultsVM() { Id = nameof(ResultsVM) }, false, IdResultsDock) },
            };
            singletons = new Dictionary<Type, ExplorerPanelVM>();
        }

        public void Close(ExplorerPanelVM view)
        {
            throw new NotImplementedException();
        }

        public void Close(Func<ExplorerPanelVM, bool> selector)
        {
            throw new NotImplementedException();
        }

        public T Open<T>(string id = null, bool openExisting = true, bool createNewExplorer = true) where T : ExplorerPanelVM
        {
            Type openType = typeof(T);
            return (T)Open(openType, id, openExisting, createNewExplorer);
        }

        public ExplorerPanelVM Open(Type explorerType, string id = null, bool openExisting = true, bool createNewExplorer = true)
        {
            ExplorerPanelVM result;
            if (openExisting)
            {
                result = FindView(main.Layout, explorerType, out var parent, id) as ExplorerPanelVM;
                if (result != null)
                {
                    if (parent != null)
                    {
                        parent.ActiveDockable = result;
                    }
                    return result;
                }
            }
            if (!createNewExplorer) return null;
            if (factory.ContainsKey(explorerType))
            {
                var (func, singleton, parentId) = factory[explorerType];
                if (singleton)
                {
                    if (!singletons.TryGetValue(explorerType, out ExplorerPanelVM panel))
                    {
                        panel = func();
                        singletons[explorerType] = panel;
                    }
                    result = panel;
                }
                else
                    result = func();
                if (result == null) return null;

                IDock parent = FindView(main.Layout, typeof(IDock), out _, parentId) as IDock;
                if (parent == null)
                {
                    parent = FindView(main.Layout, typeof(IDock), out _, IdUniversalDock) as IDock;
                }
                AddChildToDock(parent, result);
                parent.ActiveDockable = result;
                // container.GoNext.Execute(x =>
                // {
                //     result.SetHostScreen(x);
                //     return result;
                // });
                return result;
            }
            return null;
        }

        private void AddChildToDock(IDock parent, IDockable child)
        {
            if (parent == null || child == null) throw new NullReferenceException();
            if (parent.VisibleDockables == null)
            {
                parent.VisibleDockables = main.Factory.CreateList<IDockable>();
            }

            IDock hostDock;
            if (factory.ContainsKey(child.GetType()))
            {
                if (IdsMatch(factory[child.GetType()].parentId, parent.Id))
                {
                    hostDock = parent;
                }
                else
                {
                    if (!factory[child.GetType()].singleton)
                        hostDock = main.Factory.CreateDocumentDock();
                    else
                        hostDock = main.Factory.CreateToolDock();
                }
            }
            else throw new NotImplementedException();
            UpdateId(hostDock, child);
            if (hostDock.VisibleDockables == null)
            {
                hostDock.VisibleDockables = main.Factory.CreateList<IDockable>();
            }
            hostDock.Proportion = 0.2;

            IDockable hostDockable = hostDock;
            if (hostDock != parent)
            {
                main.Factory.AddDockable(hostDock, child);
            }
            else
            {
                hostDockable = child;
            }

            if (parent is IProportionalDock proportional)
            {
                if (hostDockable is IToolDock tool) tool.Alignment = Alignment.Left;
                main.Factory.InsertDockable(proportional, hostDockable, 0);
                main.Factory.InsertDockable(proportional, main.Factory.CreateSplitterDock(), 1);
            }
            else
            {
                main.Factory.AddDockable(parent, hostDockable);
            }
        }

        private void UpdateId(IDock host, IDockable child)
        {
            string id = null;
            if (factory.ContainsKey(child.GetType()))
            {
                id = factory[child.GetType()].parentId;
            }
            if (!string.IsNullOrEmpty(id))
            {
                if (host.Id == null)
                    host.Id = id;
                else if (!host.Id.Contains(id))
                    host.Id += " " + id;
            }
        }

        private bool IdsMatch(string searchedId, string foundId)
        {
            if (string.IsNullOrEmpty(searchedId)) return true;
            return foundId.Contains(searchedId);
        }

        private IDockable FindView(IDockable root, Type view, out IDock relativeParent, string id = null)
        {
            if (view.IsAssignableFrom(root.GetType()) && IdsMatch(id, root.Id))
            {
                relativeParent = null;
                return root;
            }
            return FindViewRelative(root, view, out relativeParent, id);
        }

        private IDockable FindViewRelative(IDockable parent, Type view, out IDock relativeParent, string id = null)
        {
            if (parent is IDock dock)
            {
                IDockable foundInChildren;
                IDock parentInChildren;
                if (dock.VisibleDockables != null)
                {
                    foreach (var child in dock.VisibleDockables)
                    {
                        if (view.IsAssignableFrom(child.GetType()) && IdsMatch(id, child.Id))
                        {
                            relativeParent = dock;
                            return child;
                        }
                        foundInChildren = FindViewRelative(child, view, out parentInChildren, id);
                        if (foundInChildren != null)
                        {
                            relativeParent = parentInChildren;
                            return foundInChildren;
                        }
                    }
                }
                // TODO: add support for hidden and pinned dockables.
            }
            relativeParent = null;
            return null;
        }
    }
}
