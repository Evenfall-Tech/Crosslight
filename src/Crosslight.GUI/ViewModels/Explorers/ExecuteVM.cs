using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Transformers;
using ReactiveUI;
using Splat;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExecuteVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Execute";

        public ReactiveCommand<Unit, (IFileSystemItem result, ITransformer transformer)> Translate { get; }

        public override string UrlPathSegment { get; } = "execute";
        public ViewModelActivator Activator { get; }
        public ExecuteVM() : this(null) { }
        public ExecuteVM(IScreen screen) : base(screen)
        {
            Title = ConstTitle;
            Translate = ReactiveCommand.Create<(IFileSystemItem, ITransformer)>(() =>
            {
                var locator =
                    Locator.Current
                    .GetService<IExplorerLocator>();
                ITransformer transformer =
                    locator
                    .Open<TransformersVM>()?
                    .SelectedTransformer?
                    .Transformer;
                var resultList = locator.Open<ResultListVM>();

                IFileSystemItem src;
                if (resultList.SelectedResults.Count > 1)
                {
                    src = FileSystem.FromItems(resultList
                        .SelectedResults
                        .Select(x => x.Result));
                }
                else
                {
                    src = resultList.SelectedResults.FirstOrDefault()?.Result;
                }

                if (transformer != null && src != null)
                {
                    return (transformer.Translate(src), transformer);
                }
                return (null, null);
            });

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
