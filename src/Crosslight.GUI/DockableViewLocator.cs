using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Crosslight.GUI.ViewModels;
using Dock.Model;
using ReactiveUI;
using Splat;
using System;

namespace Crosslight.GUI
{
    public class DockableViewLocator : IDataTemplate
    {
        public IControl Build(object data)
        {
            Type iViewForType = typeof(IViewFor<>).MakeGenericType(data.GetType());
            var type = Locator.Current.GetService(iViewForType);

            if (type != null)
            {
                iViewForType.GetProperty("ViewModel").SetValue(type, data);
                return type as IControl;
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + data.GetType().Name };
            }
        }

        public bool Match(object data)
        {
            return /*data is BaseViewModel || */data is IDockable;
        }
    }
}
