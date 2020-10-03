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
        public ItemsControl PropertyContainer => this.FindControl<ItemsControl>("propertyContainer");
        public Properties()
        {
            if (propertyBuilder == null)
                propertyBuilder = new PropertyBuilder();

            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.ViewModel.SelectedInstance)
                    .Subscribe(x => BuildUI(x, disposables))
                    .DisposeWith(disposables);
            });
            this.InitializeComponent();
        }

        private void BuildUI(object instance, CompositeDisposable disp)
        {
            if (instance == null)
            {
                PropertyContainer.Items = null;
                return;
            }
            var properties = instance.GetType().GetProperties();
            PropertyContainer.Items = properties.Select(x => propertyBuilder.GetControl(instance, x, this, disp));
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
