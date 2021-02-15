﻿//////////////////////////////////////////////
// Apache 2.0  - 2019
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.Converters
{
    /// <summary>
    /// This VisibilityToBoolean converter convert Visibility <-> Boolean
    /// </summary>
    public sealed class VisibilityToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value == true/*Visibility.Visible*/;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value == true
                ? true/*Visibility.Visible*/
                : false/*Visibility.Collapsed*/;
    }
}
