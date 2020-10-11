using Crosslight.GUI.ViewModels.Viewports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExplorerLocator
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
                { typeof(SourceInputVM), (() => new SourceInputVM(), true) },
                { typeof(SourcePreviewVM), (() => new SourcePreviewVM(), false) },
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

        public T Open<T>(bool createNewExplorer = true) where T : ExplorerPanelVM
        {
            Type openType = typeof(T);
            T result;
            ExplorerContainerVM container = projectViewportVM.Containers.FirstOrDefault(x => x.Top is T);
            result = container?.Top as T;
            if (result != null) return result;
            if (!createNewExplorer) return null;
            if (factory.ContainsKey(openType))
            {
                var value = factory[openType];
                if (value.singleton)
                {
                    if (!singletons.TryGetValue(openType, out ExplorerPanelVM panel))
                    {
                        panel = value.func();
                        singletons[openType] = panel;
                    }
                    result = panel as T;
                }
                else
                    result = value.func() as T;
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
    }
}
