//////////////////////////////////////////////
// Apache 2.0  - 2016-2020
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using System.Linq;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.MethodExtention
{
    public static class TrackExtention
    {
        /// <summary>
        /// Get actual top position of track
        /// </summary>
        public static double Top(this Track track)
        {
            IControl parent = track.Parent;
            if (parent == null) return 0;
            // if (track.Parent is Grid parent)
            // {
                // var topRepeatButton = parent.Children[1] as RepeatButton;
                var buttons = parent.GetVisualChildren().OfType<Button>();
                Button topRepeatButton = buttons.FirstOrDefault(x => x.Name == "PART_LineUpButton");
                if (topRepeatButton == null)
                {
                    topRepeatButton = parent.GetVisualChildren().OfType<Button>().FirstOrDefault();
                }
                if (topRepeatButton == null) return 0;
                return topRepeatButton.Height + parent.Margin.Top + 1;
            // }

            // return 0;
        }

        /// <summary>
        /// Get actual bottom position of track
        /// </summary>
        public static double Bottom(this Track track)
        {
            IControl parent = track.Parent;
            if (parent == null) return 0;
            // if (track.Parent is Grid parent)
            // {
            // var trackControl = parent.Children[2] as Track;
            //var trackControl = parent.GetVisualChildren().OfType<Track>().FirstOrDefault();

                return track.Top() +
                       track.Height +
                       parent.Margin.Top + 1;
            // }

            // return 0;
        }

        /// <summary>
        /// Get actual bottom position of track
        /// </summary>
        public static double ButtonHeight(this Track track) => track.Top() - 1;

        /// <summary>
        /// Get actual Tick Height
        /// </summary>
        public static double TickHeight(this Track track) => track.Height / track.Maximum;

        /// <summary>
        /// Get actual Tick Height with another maximum value
        /// </summary>
        public static double TickHeight(this Track track, long maximum) => track.Height / maximum;
    }
}
