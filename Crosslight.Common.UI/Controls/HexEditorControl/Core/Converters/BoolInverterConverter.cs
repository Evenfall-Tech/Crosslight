﻿//////////////////////////////////////////////
// Apache 2.0  - 2016-2018
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.Converters
{
    /// <summary>
    /// Invert bool
    /// </summary>
    public sealed class BoolInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? val = null;

            try
            {
                val = value != null && (bool)value;
            }
            catch
            {
                // ignored
            }

            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
