using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Crosslight.Common.UI.Controls
{
    public class IconButton : Button
    {
        /// <summary>
        /// Defines the <see cref="IconProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<IImage> IconProperty =
            AvaloniaProperty.Register<IconButton, IImage>(nameof(Icon));

        static IconButton()
        {
            // AffectsRender<IconButton>(IconProperty);
            // AffectsMeasure<IconButton>(IconProperty);
        }

        /// <summary>
        /// Gets or sets the icon that will be displayed.
        /// </summary>
        public IImage Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        public IconButton()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
