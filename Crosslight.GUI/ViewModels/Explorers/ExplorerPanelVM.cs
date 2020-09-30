using ReactiveUI;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExplorerPanelVM : ReactiveObject, IRoutableViewModel
    {
        protected IScreen hostScreen;
        public IScreen HostScreen => hostScreen;
        public virtual string Title { get; } = "Explorer";
        public virtual string UrlPathSegment { get; } = "explorerPanel";

        public ExplorerPanelVM(IScreen screen) => hostScreen = screen;
        public ExplorerPanelVM() { }

        public void SetHostScreen(IScreen screen) => this.RaiseAndSetIfChanged(ref hostScreen, screen, nameof(HostScreen));
    }
}
