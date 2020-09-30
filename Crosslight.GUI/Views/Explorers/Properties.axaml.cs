using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.Views.Explorers.Items;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class Properties : ReactiveUserControl<PropertiesVM>
    {
        private static PropertyBuilder propertyBuilder;
        public StackPanel PropertyContainer => this.FindControl<StackPanel>("propertyContainer");
        public Properties()
        {
            if (propertyBuilder == null)
                propertyBuilder = new PropertyBuilder();

            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.ViewModel.SelectedInstance)
                    .Where(x => x != null)
                    .Subscribe(x => BuildUI(x, disposables))
                    .DisposeWith(disposables);
            });
            this.InitializeComponent();
        }

        private void BuildUI(object instance, CompositeDisposable disp)
        {
            var properties = instance.GetType().GetProperties();
            PropertyContainer.Children.AddRange(properties.Select(x => /*propertyBuilder.GetControl(instance, x, this, disp)*/new TextBox()));
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
