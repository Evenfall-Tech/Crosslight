using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.Views.Utils
{
    [Obsolete]
    public class ControlWrapper
    {
        public Control Control { get; set; }
        public object Tag { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Size Size { get => new Size(Width, Height); }
        public ControlWrapper(Control control, object tag)
        {
            Control = control;
            var size = SizeMeasures.GetMinControlSize(control);
            Width = size.Width;
            Height = size.Height;
            Tag = tag;
        }
    }
}
