using Crosslight.GUI.ViewModels.Explorers;
using System;

namespace Crosslight.GUI.ViewModels
{
    public interface IExplorerLocator
    {
        T Open<T>(string id = null, bool openExisting = true, bool createNewExplorer = true) where T : ExplorerPanelVM;
        ExplorerPanelVM Open(Type explorerType, string id = null, bool openExisting = true, bool createNewExplorer = true);
        void Close(ExplorerPanelVM view);
        void Close(Func<ExplorerPanelVM, bool> selector);
    }
}
