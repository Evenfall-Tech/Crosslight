using Dock.Model.Controls;
using ReactiveUI;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExplorerPanelVM : Tool, IRoutableViewModel
    {
        public const string ConstTitle = "Explorer";
        protected IScreen hostScreen;
        public IScreen HostScreen => hostScreen;
        public virtual string UrlPathSegment { get; } = "explorerPanel";

        public ExplorerPanelVM(IScreen screen) => hostScreen = screen;
        public ExplorerPanelVM()
        {
            Title = ConstTitle;
        }

        public void SetHostScreen(IScreen screen) => this.RaiseAndSetIfChanged(ref hostScreen, screen, nameof(HostScreen));
    }
}
