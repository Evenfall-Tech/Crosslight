using Crosslight.API.IO;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class PropertiesVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Properties";
        protected object selectedInstance;
        protected string selectedInstanceName;
        protected readonly ObservableAsPropertyHelper<string> title;
        public object SelectedInstance
        {
            get => selectedInstance;
            set => this.RaiseAndSetIfChanged(ref selectedInstance, value);
        }
        public string SelectedInstanceName
        {
            get => selectedInstanceName;
            set => this.RaiseAndSetIfChanged(ref selectedInstanceName, value);
        }

        private string TitleValue => title.Value;

        public override string UrlPathSegment { get; } = "properties";
        public ViewModelActivator Activator { get; }
        public PropertiesVM() : this(null) { }
        public PropertiesVM(IScreen screen) : base(screen)
        {
            title = this
                .WhenAnyValue(x => x.SelectedInstanceName)
                .Select(x => string.IsNullOrWhiteSpace(x) ? ConstTitle : $"{ConstTitle} of {x}")
                .ToProperty(this, x => x.TitleValue);

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                // Can't use OAPH because instance name can be set separately.
                this.WhenAnyValue(x => x.SelectedInstance)
                    .Do(x => SelectedInstanceName = x?.GetType().Name)
                    .Subscribe()
                    .DisposeWith(disposables);
                this.WhenAnyValue(x => x.TitleValue)
                    .BindTo(this, x => x.Title)
                    .DisposeWith(disposables);
            });
        }
    }
}
