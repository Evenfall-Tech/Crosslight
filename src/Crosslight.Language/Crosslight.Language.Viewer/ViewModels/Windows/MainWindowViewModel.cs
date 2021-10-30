using Crosslight.Language.Viewer.ViewModels.Viewports;
using ReactiveUI;

namespace Crosslight.Language.Viewer.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewerViewportViewModel viewportViewModel;
        public ViewerViewportViewModel ViewportViewModel
        {
            get => viewportViewModel;
            set => this.RaiseAndSetIfChanged(ref viewportViewModel, value);
        }
    }
}
