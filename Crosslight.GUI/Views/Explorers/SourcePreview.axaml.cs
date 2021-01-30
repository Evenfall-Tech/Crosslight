using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class SourcePreview : ReactiveUserControl<SourcePreviewVM>
    {
        public TextBox SourceText => this.FindControl<TextBox>("sourceText");
        public SourcePreview()
        {
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.SourceText, x => x.SourceText.Text)
                    .DisposeWith(disposables);
            });
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
