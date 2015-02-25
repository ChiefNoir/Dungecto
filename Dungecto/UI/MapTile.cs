using Dungecto.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
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
        private Tile tile;
        private Point? nullable;
        private ControlTemplate _tileTemplate;

        /// <summary>Is tile selected?</summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public MapTile(){ }

        public MapTile(Tile description, Point position, ControlTemplate template)
        {
            var geom = new System.Windows.Shapes.Path
            {
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(description.HexColor)),
                Stroke = new SolidColorBrush(Colors.Black),
                Stretch = Stretch.Fill,
                IsHitTestVisible = false,
                Data = PathMarkupToGeometry(description.GeometryPath)
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

        Geometry PathMarkupToGeometry(string pathMarkup)
        {
            string xaml =
            "<Path " +
            "xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>" +
            "<Path.Data>" + pathMarkup + "</Path.Data></Path>";
            var path = XamlReader.Parse(xaml) as System.Windows.Shapes.Path;
            // Detach the PathGeometry from the Path
            Geometry geometry = path.Data;
            path.Data = null;
            return geometry;
        }
    }
}
