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
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class Languages : ReactiveUserControl<LanguagesVM>
    {
        public ReactiveCommand<Unit, Unit> LoadLanguage { get; set; }
        public Button LoadLanguageButton => this.FindControl<Button>("loadLanguage");
        public ListBox InputLanguages => this.FindControl<ListBox>("inputList");
        public ListBox OutputLanguages => this.FindControl<ListBox>("outputList");
        public Languages()
        {
            Locator.CurrentMutable.Register(() => new Language(), typeof(IViewFor<LanguageVM>));
            Locator.CurrentMutable.Register(() => new Language(), typeof(IViewFor<InputLanguageVM>));
            Locator.CurrentMutable.Register(() => new Language(), typeof(IViewFor<OutputLanguageVM>));
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
            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.LoadLanguage)
                    .BindTo(this, x => x.LoadLanguageButton.Command)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.InputLanguages, x => x.InputLanguages.Items)
                    .DisposeWith(disposables);
                this.Bind(ViewModel, x => x.SelectedInputLanguage, x => x.InputLanguages.SelectedItem)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.OutputLanguages, x => x.OutputLanguages.Items)
                    .DisposeWith(disposables);
                this.Bind(ViewModel, x => x.SelectedOutputLanguage, x => x.OutputLanguages.SelectedItem)
                    .DisposeWith(disposables);
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
