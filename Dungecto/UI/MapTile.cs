using Dungecto.Common.Utils;
using Dungecto.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dungecto.UI
{
    /// <summary> <seealso cref="Dungecto.UI.MapCanvas"/> tile </summary>
    public class MapTile : ContentControl
    {
        /// <summary> <see cref="IsSelected"/> DependencyProperty </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register
            (
                "IsSelected",
                typeof(bool),
                typeof(MapTile),
                new FrameworkPropertyMetadata(false)
            );

        /// <summary>Is tile selected?</summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary> Create map tile </summary>
        /// <param name="description">Tile description</param>
        /// <param name="position">Tile position</param>
        /// <param name="template">Tile template</param>
        public MapTile(Tile description, Point position, ControlTemplate template)
        {
            var geom = new System.Windows.Shapes.Path
            {
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(description.HexColor)),
                Stroke = new SolidColorBrush(Colors.Black),
                Stretch = Stretch.Fill,
                IsHitTestVisible = false,
                Data = Converters.FromPathMarkup(description.GeometryPath)
            };
            geom.Fill.Freeze();
            geom.Stroke.Freeze();
            geom.Data.Freeze();

            Height = description.Height;
            Width = description.Width;
            Template = template;
            Content = geom;

            Canvas.SetLeft(this, position.X);
            Canvas.SetTop(this, position.Y);
        }

    }
}
