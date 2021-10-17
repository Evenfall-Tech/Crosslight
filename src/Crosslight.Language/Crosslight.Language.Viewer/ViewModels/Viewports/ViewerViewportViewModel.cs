using Crosslight.Language.Viewer.ViewModels.Graph;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Crosslight.Language.Viewer.ViewModels.Viewports
{
    public class ViewerViewportViewModel : ViewModelBase, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; }

        private GraphViewerViewModel graphViewModel;
        public GraphViewerViewModel GraphViewModel
        {
            get => graphViewModel;
            set => this.RaiseAndSetIfChanged(ref graphViewModel, value);
        }

        public ViewerViewportViewModel()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                /* handle activation */
                Disposable
                    .Create(() => { /* handle deactivation */ })
                    .DisposeWith(disposables);
            });
        }
    }
}
