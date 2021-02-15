//////////////////////////////////////////////
// 2012 - Code from :
// https://stackoverflow.com/questions/11447019/is-there-any-way-to-find-the-width-of-a-character-in-a-fixed-width-font-given-t
// 
// 2018-2020 - Modified/adapted by Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Globalization;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.MethodExtention
{
    public static class StringExtension
    {
        /// <summary>
        /// Get the screen size of a string
        /// </summary>
        public static Size GetScreenSize(this string text, FontFamily fontFamily, double fontSize, FontStyle fontStyle,
            FontWeight fontWeight)
        {
            if (fontFamily == null)
            {
                fontFamily = new TextBlock().FontFamily;
            }
            fontSize = fontSize > 0 ? fontSize : new TextBlock().FontSize;

            var ft = new FormattedText(
                text ?? string.Empty,
                new Typeface(fontFamily, fontStyle, fontWeight),
                fontSize,
                TextAlignment.Left,
                TextWrapping.Wrap,
                Size.Infinity);

            return new Size(ft.Bounds.Width, ft.Bounds.Height);
        }
    }
}
