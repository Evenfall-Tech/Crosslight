using Crosslight.API.Transformers;
using ReactiveUI;
using Splat;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class TransformerVM : ReactiveObject
    {
        protected string path;
        protected string title;
        protected ITransformer transformer;
        public ITransformer Transformer
        {
            get => transformer;
            set => this.RaiseAndSetIfChanged(ref transformer, value);
        }
        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }
        protected IObservable<bool> SelectCommandAvailable => this
            .WhenAnyValue(x => x.Transformer, x => x.Path, (transformer, path) => transformer != null && !string.IsNullOrWhiteSpace(path));
        public ReactiveCommand<Unit, Unit> SelectCommand => ReactiveCommand.Create(() =>
        {
            var locator = Locator.Current.GetService<IExplorerLocator>();
            var props = locator.Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
            if (props != null)
            {
                props.SelectedInstance = Transformer.Options;
            }
            var transformer = locator.Open<TransformersVM>();
            if (transformer != null)
            {
                transformer.SelectedTransformer = this;
            }
        },
        SelectCommandAvailable);
        public ReactiveCommand<Unit, Unit> RemoveCommand => ReactiveCommand.Create(() =>
        {
            var locator = Locator.Current.GetService<IExplorerLocator>();
            var props = locator.Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
            if (props != null)
            {
                if (props.SelectedInstance == Transformer.Options)
                    props.SelectedInstance = null;
            }
            var transformer = locator.Open<TransformersVM>();
            if (transformer != null)
            {
                transformer.RemoveTransformer.Execute(this).Subscribe();
            }
        },
        SelectCommandAvailable);
    }
}
