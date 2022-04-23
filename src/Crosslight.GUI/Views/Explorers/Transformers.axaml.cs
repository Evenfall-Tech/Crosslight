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
    public class Transformers : ReactiveUserControl<TransformersVM>
    {
        public ReactiveCommand<Unit, Unit> LoadTransformer { get; set; }
        public ItemsControl TransformerTypeList => this.FindControl<ItemsControl>("transformerTypeList");
        public Button LoadTransformerButton => this.FindControl<Button>("loadTransformer");
        public Transformers()
        {
            Locator.CurrentMutable.Register(() => new TransformerType(), typeof(IViewFor<TransformerTypeVM>));
            LoadTransformer = ReactiveCommand.CreateFromTask(async () =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Choose transformer files",
                    AllowMultiple = true
                };
                Window window = GetWindow();
                if (window == null) return;
                var outPathStrings = await openFileDialog.ShowAsync(window);
                if (outPathStrings.Length == 0) return;
                foreach (string s in outPathStrings)
                {
                    await ViewModel.AddTransformer.Execute(s);
                }
            });
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.TransformerTypes, x => x.TransformerTypeList.Items)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.LoadTransformer)
                    .BindTo(this, x => x.LoadTransformerButton.Command)
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
