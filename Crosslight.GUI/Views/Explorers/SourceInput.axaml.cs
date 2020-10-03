using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers;
using ReactiveUI;

namespace Crosslight.GUI.Views.Explorers
{
    public class SourceInput : ReactiveUserControl<SourceInputVM>
    {
        public SourceInput()
        {
            this.WhenActivated(disp =>
            {

            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
