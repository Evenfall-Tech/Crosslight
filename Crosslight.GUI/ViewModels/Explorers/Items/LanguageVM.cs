using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public abstract class LanguageVM : ReactiveObject
    {
        protected string path;
        protected string title;
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
        protected abstract IObservable<bool> SelectCommandAvailable { get; }
        public abstract ReactiveCommand<Unit, Unit> SelectCommand { get; }
        public abstract ReactiveCommand<Unit, Unit> RemoveCommand { get; }
    }
}
