using ReactiveUI;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExplorerPanelVM : ReactiveObject, IRoutableViewModel
    {
        public const string ConstTitle = "Explorer";
        protected string id;
        protected IScreen hostScreen;
        public string ID
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }
        public IScreen HostScreen => hostScreen;
        public virtual string Title { get; } = ConstTitle;
        public virtual string UrlPathSegment { get; } = "explorerPanel";

        public ExplorerPanelVM(IScreen screen) => hostScreen = screen;
        public ExplorerPanelVM() { }

        public void SetHostScreen(IScreen screen) => this.RaiseAndSetIfChanged(ref hostScreen, screen, nameof(HostScreen));
    }
}
