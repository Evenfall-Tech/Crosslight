using Crosslight.API.Lang;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class OutputLanguageVM : LanguageVM
    {
        protected OutputLanguage outputLanguage;
        public OutputLanguage OutputLanguage
        {
            get => outputLanguage;
            set => this.RaiseAndSetIfChanged(ref outputLanguage, value);
        }

        public override ReactiveCommand<Unit, Unit> SelectCommand => ReactiveCommand.Create(() =>
        {
            var locator = Locator.Current.GetService<ExplorerLocator>();
            var props = locator.Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
            if (props != null)
            {
                props.SelectedInstance = OutputLanguage.Options;
            }
            var lang = locator.Open<LanguagesVM>();
            if (lang != null)
            {
                lang.SelectedOutputLanguage = this;
            }
        },
        SelectCommandAvailable);

        public override ReactiveCommand<Unit, Unit> RemoveCommand => ReactiveCommand.Create(() =>
        {
            var locator = Locator.Current.GetService<ExplorerLocator>();
            var props = locator.Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
            if (props != null)
            {
                if (props.SelectedInstance == OutputLanguage.Options)
                    props.SelectedInstance = null;
            }
            var lang = locator.Open<LanguagesVM>();
            if (lang != null)
            {
                lang.RemoveLanguage.Execute(this).Subscribe();
            }
        },
        SelectCommandAvailable);

        protected override IObservable<bool> SelectCommandAvailable => this
            .WhenAnyValue(x => x.OutputLanguage, x => x.Path, x => x.OutputLanguage.Options)
            .Select(k => k.Item1 != null && !string.IsNullOrWhiteSpace(k.Item2) && k.Item3 != null);
    }
}
