using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.Common.UI.Controls;
using Crosslight.GUI.Util;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers.Items
{
    public class ResultItem : ReactiveUserControl<ResultItemVM>
    {
        private static IAssetLoader assets;
        private static IAssetLoader Assets
        {
            get
            {
                if (assets == null) assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                return assets;
            }
        }

        public TextBlock Title => this.FindControl<TextBlock>("title");
        public Button Remover => this.FindControl<Button>("remove");
        public IconButton IconExpand => this.FindControl<IconButton>("expand");
        public Image ResultIcon => this.FindControl<Image>("icon");
        public Control MainControl => this.FindControl<Control>("main");
        public TreeView Tree => this.FindControl<TreeView>("tree");
        public ReactiveCommand<Unit, ResultItemState> CommandExpand
            => ReactiveCommand.Create(() => ViewModel.State = ResultItemVM.SwitchState(ViewModel.State));
        public ResultItem()
        {
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Name, x => x.Title.Text)
                    .DisposeWith(disp);

                this.WhenAnyValue(x => x.ViewModel.Result)
                    .Select(x => ItemsFromFile(x))
                    .BindTo(this, x => x.Tree.Items)
                    .DisposeWith(disp);

                this.WhenAnyValue(x => x.ViewModel.State)
                    .Select(x => IconExpandFromState(x))
                    .BindTo(this, x => x.IconExpand.Icon)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.ViewModel.State)
                    .Select(x => VisibleFromState(x))
                    .BindTo(this, x => x.Tree.IsVisible);

                this.WhenAnyValue(x => x.ViewModel.Result, x => x.ViewModel.State, (result, state) => (result, state))
                    .Select(x => IconFromFile(x.result, x.state))
                    .BindTo(this, x => x.ResultIcon.Source)
                    .DisposeWith(disp);

                this.WhenAnyValue(x => x.CommandExpand)
                    .BindTo(this, x => x.IconExpand.Command)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.ViewModel.IsTopLevel)
                    .BindTo(this, x => x.Remover.IsVisible)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.ViewModel.RemoveCommand)
                    .BindTo(this, x => x.Remover.Command)
                    .DisposeWith(disp);

                Observable.FromEventPattern<EventHandler<RoutedEventArgs>, RoutedEventArgs>
                    (d => MainControl.DoubleTapped += d, d => MainControl.DoubleTapped -= d)
                    // TODO: this can be refactored with side-effects.
                    .Select(async x => await ViewModel.OpenCommand.ExecuteIfPossible())
                    .Subscribe()
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.Tree.SelectedItem)
                    .DistinctUntilChanged()
                    .Where(x => x != null)
                    .Select<object, object>(x => null)
                    .BindTo(this, x => x.Tree.SelectedItem)
                    .DisposeWith(disp);
            });
            InitializeComponent();
        }

        private IEnumerable<ResultItemVM> ItemsFromFile(IFileSystemItem file)
        {
            if (file is null) return null;
            if (file is IDirectory directory) return directory.Items.Select(x => new ResultItemVM()
            {
                Name = x is IPhysicalFile physical ? Path.GetFileName(physical.Path) : x.Name,
                Origin = ViewModel.Origin,
                Result = x,
                IsTopLevel = false,
            });
            if (file is IFile) return null;
            else throw new NotImplementedException($"{file.GetType().Name} file is not yet supported.");
        }

        private bool VisibleFromState(ResultItemState state)
        {
            return state switch
            {
                ResultItemState.Expanded => true,
                _ => false,
            };
        }

        private IImage IconExpandFromState(ResultItemState state)
        {
            string path = state switch
            {
                ResultItemState.Collapsed => "avares://Crosslight.Common.UI/Assets/Icons/Editor/GlyphRight_16x.png",
                ResultItemState.Expanded => "avares://Crosslight.Common.UI/Assets/Icons/Editor/ScrollbarArrowsDownRight_16x.png",
                _ => null,
            };
            if (path == null) return null;
            return new Bitmap(Assets.Open(new Uri(path)));
        }

        private IImage IconFromFile(IFileSystemItem file, ResultItemState state)
        {
            if (file == null) throw new NullReferenceException();
            string path;
            if (file is IFile) path = "avares://Crosslight.Common.UI/Assets/Icons/Editor/Document_16x.png";
            else if (file is IDirectory)
            {
                path = state switch
                {
                    ResultItemState.Collapsed => "avares://Crosslight.Common.UI/Assets/Icons/Editor/FolderClosed_16x.png",
                    ResultItemState.Expanded => "avares://Crosslight.Common.UI/Assets/Icons/Editor/FolderOpened_16x.png",
                    ResultItemState.NonExpandable => "avares://Crosslight.Common.UI/Assets/Icons/Editor/DocumentGroup_16x.png",
                    _ => null,
                };
            }
            else throw new NotImplementedException($"{file.GetType().Name} file is not yet supported.");
            if (path == null) return null;
            return new Bitmap(Assets.Open(new Uri(path)));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
