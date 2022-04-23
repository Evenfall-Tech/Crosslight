using Crosslight.API.Transformers;
using Crosslight.Common.Runtime;
using Crosslight.GUI.ViewModels.Explorers.Items;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class TransformersVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Transformers";
        protected SourceCache<TransformerVM, string> transformerSource;
        protected ObservableCollection<TransformerTypeVM> transformerTypes;
        protected TransformerVM selectedTransformer;

        public ObservableCollection<TransformerTypeVM> TransformerTypes => transformerTypes;
        public TransformerVM SelectedTransformer
        {
            get => selectedTransformer;
            set
            {
                if (value != null && value != selectedTransformer)
                {
                    this.RaiseAndSetIfChanged(ref selectedTransformer, value);
                    PropertiesVM properties = Locator.Current.GetService<IExplorerLocator>().Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
                    if (properties != null)
                        properties.SelectedInstance = selectedTransformer.Transformer.Options;
                }
            }
        }
        public ReactiveCommand<string, Unit> AddTransformer { get; }
        public ReactiveCommand<TransformerVM, Unit> RemoveTransformer { get; }

        public override string UrlPathSegment { get; } = "transformers";
        public ViewModelActivator Activator { get; }
        public TransformersVM() : this(null) { }
        public TransformersVM(IScreen screen) : base(screen)
        {
            Title = ConstTitle;
            transformerSource = new SourceCache<TransformerVM, string>(x => x.Title);
            AddTransformer = ReactiveCommand.Create((string s) =>
            {
                if (string.IsNullOrWhiteSpace(s)) return;
                var assembly = TypeLocator.LoadAssembly(s);
                if (assembly != null)
                {
                    Type[] transformers = TypeLocator.FindTypesInAssembly<ITransformer>(assembly);
                    if (transformers != null && transformers.Length > 0)
                    {
                        foreach (var trans in transformers)
                        {
                            ITransformer transformer = TypeLocator.CreateTypeInstance<ITransformer>(trans);
                            if (transformer != null)
                            {
                                var type = transformer.TransformerType;
                                TransformerVM vmToAdd = null;
                                vmToAdd = new TransformerVM()
                                {
                                    Path = s,
                                    Title = transformer.Name,
                                    Transformer = transformer,
                                };
                                if (vmToAdd != null)
                                {
                                    transformerSource.AddOrUpdate(vmToAdd);
                                }
                            }
                        }
                    }
                }
            }, Observable.Return(true));
            RemoveTransformer = ReactiveCommand.Create((TransformerVM transformer) =>
            {
                if (SelectedTransformer == transformer)
                    SelectedTransformer = null;
                transformerSource.Remove(transformer);
            }, Observable.Return(true));

            transformerTypes = new ObservableCollection<TransformerTypeVM>();
            var transformerEnumValues = ((TransformerType[])Enum.GetValues(typeof(TransformerType))).Except(
                new TransformerType[]
                {
                    TransformerType.None,
                }
            );
            foreach (var transformerType in transformerEnumValues)
            {
                transformerTypes.Add(new TransformerTypeVM()
                {
                    TransformerType = transformerType,
                });
            }

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                var observable = transformerSource.Connect();
                foreach (var transformerType in transformerEnumValues)
                {
                    var transformerTypeVM = transformerTypes.FirstOrDefault(x => x.TransformerType == transformerType);
                    if (transformerTypeVM != null)
                    {
                        observable
                            .Filter(x => x?.Transformer?.TransformerType == transformerType)
                            .Bind(out var transformersObservable)
                            .Subscribe()
                            .DisposeWith(disposables);
                        transformerTypeVM.Transformers = transformersObservable;
                        this.WhenAnyValue(x => x.SelectedTransformer)
                            .DistinctUntilChanged()
                            .Do(x => Console.WriteLine(x?.Path))
                            .BindTo(transformerTypeVM, x => x.Selected)
                            .DisposeWith(disposables);
                    }
                }
            });
        }
    }
}
