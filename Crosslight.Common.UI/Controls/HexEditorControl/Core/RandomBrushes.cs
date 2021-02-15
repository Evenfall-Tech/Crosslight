//////////////////////////////////////////////
// Apache 2.0 - 2021
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia.Media;
using System;
using System.Reflection;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core
{
    public static class RandomBrushes
    {
        /// <summary>
        /// Pick a random bruch
        /// </summary>
        public static ISolidColorBrush PickBrush()
        {
            PropertyInfo[] properties = typeof(Brushes).GetProperties();

            return (ISolidColorBrush)properties
                [
                    new Random().Next(properties.Length)
                ].GetValue(null, null);
        }
    }
}
