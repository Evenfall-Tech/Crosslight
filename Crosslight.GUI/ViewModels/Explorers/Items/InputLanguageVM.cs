using Crosslight.API.Lang;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class InputLanguageVM : LanguageVM
    {
        protected InputLanguage inputLanguage;
        public InputLanguage InputLanguage
        {
            get => inputLanguage;
            set => this.RaiseAndSetIfChanged(ref inputLanguage, value);
        }

        public override ReactiveCommand<Unit, Unit> SelectCommand => ReactiveCommand.Create(() =>
        {
            var props = Locator.Current.GetService<ExplorerLocator>().Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
            if (props != null)
            {
                props.SelectedInstance = InputLanguage.Options;
            }
            var lang = Locator.Current.GetService<ExplorerLocator>().Open<LanguagesVM>();
            if (lang != null)
            {
                lang.SelectedInputLanguage = this;
            }
        },
        SelectCommandAvailable);

        public override ReactiveCommand<Unit, Unit> RemoveCommand => ReactiveCommand.Create(() =>
        {
            var props = Locator.Current.GetService<ExplorerLocator>().Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
            if (props != null)
            {
                if (props.SelectedInstance == InputLanguage.Options)
                    props.SelectedInstance = null;
            }
            var lang = Locator.Current.GetService<ExplorerLocator>().Open<LanguagesVM>();
            if (lang != null)
            {
                lang.RemoveLanguage.Execute(this).Subscribe();
            }
        },
        SelectCommandAvailable);

        protected override IObservable<bool> SelectCommandAvailable => this
            .WhenAnyValue(x => x.InputLanguage, x => x.Path, x => x.InputLanguage.Options)
            .Select(k => k.Item1 != null && !string.IsNullOrWhiteSpace(k.Item2) && k.Item3 != null);
    }
}
