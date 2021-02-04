using Crosslight.GUI.ViewModels.Viewports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExplorerLocator : IExplorerLocator
    {
        private readonly ProjectViewportVM projectViewportVM;
        private readonly Dictionary<Type, (Func<ExplorerPanelVM> func, bool singleton)> factory;
        private readonly Dictionary<Type, ExplorerPanelVM> singletons;
        public ExplorerLocator(ProjectViewportVM viewportVM)
        {
            projectViewportVM = viewportVM;
            factory = new Dictionary<Type, (Func<ExplorerPanelVM> func, bool singleton)>()
            {
                { typeof(LanguagesVM), (() => new LanguagesVM(), true) },
                { typeof(PropertiesVM), (() => new PropertiesVM(), true) },
                { typeof(ExecuteVM), (() => new ExecuteVM(), true) },
                { typeof(ResultListVM), (() => new ResultListVM(), true) },
                { typeof(ResultsVM), (() => new ResultsVM(), false) },
            };
            singletons = new Dictionary<Type, ExplorerPanelVM>();
        }

        public ExplorerLocator Register(Func<ExplorerPanelVM> constructor, Type type)
        {
            factory[type] = (constructor, false);
            return this;
        }

        public ExplorerLocator RegisterLazySingleton(Func<ExplorerPanelVM> constructor, Type type)
        {
            factory[type] = (constructor, true);
            return this;
        }

        public T Open<T>(string id = null, bool openExisting = true, bool createNewExplorer = true) where T : ExplorerPanelVM
        {
            Type openType = typeof(T);
            return (T)Open(openType, id, openExisting, createNewExplorer);
        }

        public ExplorerPanelVM Open(Type explorerType, string id = null, bool openExisting = true, bool createNewExplorer = true)
        {
            ExplorerPanelVM result;
            ExplorerContainerVM container;
            if (openExisting)
            {
                container = projectViewportVM.Containers.FirstOrDefault(
                    x => 
                    x != null && x.Top != null &&
                    explorerType.IsAssignableFrom(x.Top.GetType()) && 
                    (id == null || x.ID == id));
                result = container?.Top;
                if (result != null) return result;
            }
            if (!createNewExplorer) return null;
            if (factory.ContainsKey(explorerType))
            {
                var (func, singleton) = factory[explorerType];
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

                container = new ExplorerContainerVM();
                projectViewportVM.AddExplorer(container);
                container.GoNext.Execute(x =>
                {
                    result.SetHostScreen(x);
                    return result;
                });
                return result;
            }
            return null;
        }

        public void Close(ExplorerPanelVM panel)
        {
            var container = projectViewportVM.Containers.FirstOrDefault(x => x.Top == panel);
            if (container != null)
                projectViewportVM.RemoveExplorer(container);
        }

        public void Close(ExplorerContainerVM container)
        {
            var cont = projectViewportVM.Containers.FirstOrDefault(x => x == container);
            if (cont != null)
                projectViewportVM.RemoveExplorer(cont);
        }

        public void Close(Func<ExplorerPanelVM, bool> selector)
        {
            Close((ExplorerContainerVM container) =>
            {
                return selector(container.Top);
            });
        }

        public void Close(Func<ExplorerContainerVM, bool> selector)
        {
            var toRemove = projectViewportVM.Containers.Where(selector).ToArray();
            foreach (var c in toRemove) if (c != null) Close(c);
        }
    }
}
