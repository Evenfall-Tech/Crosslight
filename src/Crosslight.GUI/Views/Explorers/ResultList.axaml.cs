using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using Crosslight.GUI.Views.Explorers.Items;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.IO;
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
        public ReactiveCommand<Unit, Unit> LoadSourceFileGroup { get; set; }
        public ReactiveCommand<Unit, Unit> LoadSourceFolder { get; set; }
        public Button LoadFileSourceButton => this.FindControl<Button>("loadFileSrc");
        public Button LoadFileGroupSourceButton => this.FindControl<Button>("loadFileGroupSrc");
        public Button LoadFolderSourceButton => this.FindControl<Button>("loadFolderSrc");
        public ResultList()
        {
            Locator.CurrentMutable.Register(() => new ResultType(), typeof(IViewFor<ResultTypeVM>));
            InitializeCommands();
            this.WhenActivated(disp =>
            {
                this.WhenAnyValue(x => x.LoadSourceFile)
                    .BindTo(this, x => x.LoadFileSourceButton.Command)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.LoadSourceFileGroup)
                    .BindTo(this, x => x.LoadFileGroupSourceButton.Command)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.LoadSourceFolder)
                    .BindTo(this, x => x.LoadFolderSourceButton.Command)
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

        private void InitializeCommands()
        {
            LoadSourceFile = ReactiveCommand.CreateFromTask(async () =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Choose input file",
                    AllowMultiple = false
                };
                Window window = GetWindow();
                if (window == null) return;
                var outPathStrings = await openFileDialog.ShowAsync(window);
                IFileSystemItem fileSystemItem;
                string name;

                if (outPathStrings.Length == 0) return;
                else if (outPathStrings.Length == 1)
                {
                    fileSystemItem = FileSystem.FromFile(outPathStrings[0]);
                    name = Path.GetFileName((fileSystemItem as IPhysicalFile).Path);
                }
                else
                {
                    fileSystemItem = FileSystem.FromFiles(outPathStrings);
                    name = fileSystemItem.Name;
                }
                await ViewModel.AddResultVM.Execute(new ResultItemVM()
                {
                    Name = name,
                    Origin = API.Lang.LanguageType.Input,
                    Result = fileSystemItem,
                });
            });
            LoadSourceFileGroup = ReactiveCommand.CreateFromTask(async () =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Choose input files",
                    AllowMultiple = true
                };
                Window window = GetWindow();
                if (window == null) return;
                var outPathStrings = await openFileDialog.ShowAsync(window);
                IFileSystemItem fileSystemItem;
                string name;

                if (outPathStrings.Length == 0) return;
                else if (outPathStrings.Length == 1)
                {
                    fileSystemItem = FileSystem.FromFile(outPathStrings[0]);
                    name = Path.GetFileName((fileSystemItem as IPhysicalFile).Path);
                }
                else
                {
                    fileSystemItem = FileSystem.FromFiles(outPathStrings);
                    name = fileSystemItem.Name;
                }
                await ViewModel.AddResultVM.Execute(new ResultItemVM()
                {
                    Name = name,
                    Origin = API.Lang.LanguageType.Input,
                    Result = fileSystemItem,
                });
            });
            LoadSourceFolder = ReactiveCommand.CreateFromTask(async () =>
            {
                OpenFolderDialog openFolderDialog = new OpenFolderDialog
                {
                    Title = "Choose input folder",
                };
                Window window = GetWindow();
                if (window == null) return;
                var outPathString = await openFolderDialog.ShowAsync(window);

                var fileSystemItem = FileSystem.FromFolder(outPathString);
                string fullPath = Path.GetFullPath(fileSystemItem.Name).TrimEnd(Path.DirectorySeparatorChar);
                string name = Path.GetFileName(fullPath);

                await ViewModel.AddResultVM.Execute(new ResultItemVM()
                {
                    Name = name,
                    Origin = API.Lang.LanguageType.Input,
                    Result = fileSystemItem,
                });
            });
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
