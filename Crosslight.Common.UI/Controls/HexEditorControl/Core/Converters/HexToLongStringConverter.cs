//////////////////////////////////////////////
// Apache 2.0  - 2016-2018
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia.Data.Converters;
using Crosslight.Common.UI.Controls.HexEditorControl.Core.Bytes;
using System;
using System.Globalization;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.Converters
{
    /// <summary>
    /// Used to convert hexadecimal to Long value.
    /// </summary>
    public sealed class HexToLongStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return string.Empty;

            var (success, val) = ByteConverters.IsHexValue(value.ToString());

            return success
                ? (object)val
                : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
