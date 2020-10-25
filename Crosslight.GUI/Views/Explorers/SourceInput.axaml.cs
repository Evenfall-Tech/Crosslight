using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.Util;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using Crosslight.GUI.Views.Explorers.Items;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class SourceInput : ReactiveUserControl<SourceInputVM>
    {
        public ObservableCollection<SourceVM> SelectedItems { get; }

        public ReactiveCommand<Unit, Unit> LoadSourceFile { get; set; }
        public Button LoadSourceFileButton => this.FindControl<Button>("loadFileSrc");
        public ListBox InputFiles => this.FindControl<ListBox>("sourceList");
        public SourceInput()
        {
            Locator.CurrentMutable.Register(() => new SourceItem(), typeof(IViewFor<SourceVM>));

            SelectedItems = new ObservableCollection<SourceVM>();
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
                foreach (string s in outPathStrings)
                {
                    await ViewModel.AddSource.Execute(SourceVM.FromFile(s));
                }
            });
            this.WhenActivated(disp =>
            {
                this.WhenAnyValue(x => x.LoadSourceFile)
                    .BindTo(this, x => x.LoadSourceFileButton.Command)
                    .DisposeWith(disp);
                this.OneWayBind(ViewModel, x => x.FileSources, x => x.InputFiles.Items)
                    .DisposeWith(disp);
                Observable
                    .FromEventPattern<EventHandler<SelectionChangedEventArgs>, SelectionChangedEventArgs>
                        (h => InputFiles.SelectionChanged += h, h => InputFiles.SelectionChanged -= h)
                    .Subscribe(x =>
                    {
                        if (x != null && x.EventArgs != null)
                        {
                            ViewModel.SelectedSources.RemoveMany(x.EventArgs.RemovedItems.OfType<SourceVM>());
                            ViewModel.SelectedSources.AddRange(x.EventArgs.AddedItems.OfType<SourceVM>());
                        }
                    })
                    .DisposeWith(disp);
                InputFiles
                    .WhenAnyValue(x => x.SelectedItem)
                    .Where(x => x is SourceVM)
                    .Select(x => x as SourceVM)
                    .Select(async x => await x.SelectCommand.ExecuteIfPossible())
                    .Subscribe()
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.InputFiles.SelectedItems)
                    .Subscribe(x => Console.WriteLine(x))
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
