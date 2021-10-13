using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using Crosslight.GUI.Views.Explorers.Items;
using ReactiveUI;
using Splat;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class Languages : ReactiveUserControl<LanguagesVM>
    {
        public ReactiveCommand<Unit, Unit> LoadLanguage { get; set; }
        public ItemsControl LanguageTypeList => this.FindControl<ItemsControl>("languageTypeList");
        public Button LoadLanguageButton => this.FindControl<Button>("loadLanguage");
        public Languages()
        {
            Locator.CurrentMutable.Register(() => new LanguageType(), typeof(IViewFor<LanguageTypeVM>));
            LoadLanguage = ReactiveCommand.CreateFromTask(async () =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Choose language files",
                    AllowMultiple = true
                };
                Window window = GetWindow();
                if (window == null) return;
                var outPathStrings = await openFileDialog.ShowAsync(window);
                if (outPathStrings.Length == 0) return;
                foreach (string s in outPathStrings)
                {
                    await ViewModel.AddLanguage.Execute(s);
                }
            });
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.LanguageTypes, x => x.LanguageTypeList.Items)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.LoadLanguage)
                    .BindTo(this, x => x.LoadLanguageButton.Command)
                    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private Window GetWindow()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                return desktopLifetime.MainWindow;
            }
            return null;
        }
    }
}
