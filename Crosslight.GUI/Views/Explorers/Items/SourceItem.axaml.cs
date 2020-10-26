using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.Util;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers.Items
{
    public class SourceItem : ReactiveUserControl<SourceVM>
    {
        public TextBlock Title => this.FindControl<TextBlock>("title");
        //public Button Clicker => this.FindControl<Button>("clicker");
        public Button Remover => this.FindControl<Button>("remove");
        public SourceItem()
        {
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Title, x => x.Title.Text)
                    .DisposeWith(disp);
                Observable.FromEventPattern<EventHandler<RoutedEventArgs>, RoutedEventArgs>
                    (d => this.DoubleTapped += d, d => this.DoubleTapped -= d)
                    .Select(async x => await ViewModel.OpenCommand.ExecuteIfPossible())
                    .Subscribe()
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.ViewModel.RemoveCommand)
                    .BindTo(this, x => x.Remover.Command)
                    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
