using Crosslight.Transformer.Viewer.ViewModels.Viewports;
using ReactiveUI;

namespace Crosslight.Transformer.Viewer.ViewModels.Windows
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
