using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;

namespace Crosslight.GUI.Views.Explorers.Items
{
    public class PropertyBuilder
    {
        private delegate IControl ControlFactory(
            object infoFor, 
            PropertyInfo info, 
            IViewFor view, 
            CompositeDisposable disp);
        private Dictionary<Type, ControlFactory> factoryDict;

        public PropertyBuilder()
        {
            factoryDict = new Dictionary<Type, ControlFactory>()
            {
                { typeof(bool), BoolProperty },
            };
        }

        public IControl GetControl(object infoFor, PropertyInfo info, IViewFor view, CompositeDisposable disp)
        {
            if (info == null) return null;
            if (factoryDict.ContainsKey(info.PropertyType))
                return factoryDict[info.PropertyType](infoFor, info, view, disp);
            return DefaultProperty(infoFor, info, view, disp);
        }

        private IControl InsertIntoContainer(
            IControl control,
            object infoFor,
            PropertyInfo info,
            IViewFor view,
            CompositeDisposable disp)
        {
            Grid container = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitions("*,2*"),
            };
            Border borderName = new Border()
            {
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.DarkGray),
            };
            borderName.SetValue(Grid.ColumnProperty, 0);
            Border borderValue = new Border()
            {
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.DarkGray),
            };
            borderValue.SetValue(Grid.ColumnProperty, 1);

            TextBlock name = new TextBlock()
            {
                Text = info.Name,
                //IsReadOnly = true,
                //BorderThickness = new Thickness(Double.Epsilon),
            };
            borderName.Child = name;
            container.Children.Add(borderName);

            Grid child = new Grid();
            child.SetValue(Layoutable.MarginProperty, new Thickness(2.0, 1.0));
            child.Children.Add(control);
            borderValue.Child = child;
            container.Children.Add(borderValue);

            return container;
        }

        private IControl DefaultProperty(object infoFor, PropertyInfo info, IViewFor view, CompositeDisposable disp)
        {
            TextBox text = new TextBox()
            {
                IsReadOnly = true,
                BorderThickness = new Thickness(0.0),
                Background = new SolidColorBrush(Colors.Transparent),
                AcceptsReturn = false,
                TextWrapping = TextWrapping.NoWrap,
            };
            if (info.CanRead) text.Text = (string)info.GetValue(infoFor).ToString();
            else text.Text = "<Undefined>";
            return InsertIntoContainer(text, infoFor, info, view, disp);
        }

        private IControl BoolProperty(object infoFor, PropertyInfo info, IViewFor view, CompositeDisposable disp)
        {
            CheckBox toggle = new CheckBox
            {
                IsChecked = info.CanRead ? (bool)info.GetValue(infoFor) : false,
                IsEnabled = info.CanWrite,
            };
            if (info.CanWrite)
            {
                toggle.Command = ReactiveCommand.Create(
                    (bool state) =>
                    {
                        info.SetValue(infoFor, state);
                    },
                    Observable.Return(true));
            }
            return InsertIntoContainer(toggle, infoFor, info, view, disp);
        }
    }
}
