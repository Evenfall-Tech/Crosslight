using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Crosslight.GUI.ViewModels
{
    public class MenuItemVM : BaseViewModel
    {
        private string header;
        private ICommand command;
        private object commandParameter;
        private ObservableCollectionExtended<MenuItemVM> items;
        public string Header
        {
            get => header;
            set => this.RaiseAndSetIfChanged(ref header, value);
        }
        public ICommand Command
        {
            get => command;
            set => this.RaiseAndSetIfChanged(ref command, value);
        }
        public object CommandParameter
        {
            get => commandParameter;
            set => this.RaiseAndSetIfChanged(ref commandParameter, value);
        }
        public ObservableCollectionExtended<MenuItemVM> Items
        {
            get => items;
            set => this.RaiseAndSetIfChanged(ref items, value);
        }

        public MenuItemVM()
        {
            Items = new ObservableCollectionExtended<MenuItemVM>();
        }
    }
}
