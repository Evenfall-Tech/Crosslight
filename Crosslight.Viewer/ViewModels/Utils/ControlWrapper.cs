using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.ViewModels.Utils
{
    public class ControlWrapper
    {
        public Control Control { get; set; }
        public object Tag { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public ControlWrapper(Control control, object tag)
        {
            Control = control;
            Width = control.Bounds.Width;
            Height = control.Bounds.Height;
            Tag = tag;
        }
    }
}
