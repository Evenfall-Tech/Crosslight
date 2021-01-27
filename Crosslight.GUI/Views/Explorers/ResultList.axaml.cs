using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.API.IO.FileSystem;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using Crosslight.GUI.Views.Explorers.Items;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class ResultList : ReactiveUserControl<ResultListVM>
    {
        public ItemsControl ResultTypeList => this.FindControl<ItemsControl>("resultTypeList");
        public ReactiveCommand<Unit, Unit> LoadSourceFile { get; set; }
        public Button LoadFileSourceButton => this.FindControl<Button>("loadFileSrc");
        public ResultList()
        {
            Locator.CurrentMutable.Register(() => new ResultType(), typeof(IViewFor<ResultTypeVM>));
            LoadSourceFile = ReactiveCommand.CreateFromTask(async () =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Choose input files",
                    AllowMultiple = true
                };
                Window window = GetWindow();
                if (window == null) return;
                var outPathStrings = await openFileDialog.ShowAsync(window);
                if (outPathStrings.Length == 0) return;

                var filesToAdd = FileSystem.FromFiles(outPathStrings);
                await ViewModel.AddResultVM.Execute(new ResultItemVM()
                {
                    Name = filesToAdd.Name,
                    Origin = API.Lang.LanguageType.Input,
                    Result = filesToAdd,
                });
            });
            this.WhenActivated(disp =>
            {
                this.WhenAnyValue(x => x.LoadSourceFile)
                    .BindTo(this, x => x.LoadFileSourceButton.Command)
                    .DisposeWith(disp);
                this.OneWayBind(ViewModel, x => x.ResultTypes, x => x.ResultTypeList.Items)
                    .DisposeWith(disp);
                //Observable
                //    .FromEventPattern<EventHandler<SelectionChangedEventArgs>, SelectionChangedEventArgs>
                //        (h => ResultListInter.SelectionChanged += h, h => ResultListInter.SelectionChanged -= h)
                //    .Subscribe(x =>
                //    {
                //        if (x != null && x.EventArgs != null)
                //        {
                //            ViewModel.SelectedIntermediateResults.RemoveMany(x.EventArgs.RemovedItems.OfType<ResultItemVM>());
                //            ViewModel.SelectedIntermediateResults.AddRange(x.EventArgs.AddedItems.OfType<ResultItemVM>());
                //        }
                //    })
                //    .DisposeWith(disp);
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
