using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class LanguageVM : ReactiveObject
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
    }
}
