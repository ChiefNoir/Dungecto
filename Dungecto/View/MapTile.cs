using Dungecto.Common.Utils;
using Dungecto.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dungecto.View
{
    /// <summary> <seealso cref="Dungecto.View.MapCanvas"/> tile </summary>
    public class MapTile : ContentControl
    {
        /// <summary> <see cref="IsSelected"/> DependencyProperty </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register
            (
                "IsSelected",
                typeof(bool),
                typeof(MapTile),
                new FrameworkPropertyMetadata(false, OnIsSelectedPropertyChanged)
            );

        /// <summary>Is tile selected?</summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public Tile Tile
        {
            get { return (Tile)GetValue(TileProperty); }
            set { SetValue(TileProperty, value); }
        }

        public static readonly DependencyProperty TileProperty = DependencyProperty.Register
            (
                "Tile",
                typeof(Tile),
                typeof(MapTile),
                new PropertyMetadata(OnTilePropertyChanged)
            );

        private static void OnTilePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as MapTile;
            if (control != null)
            {
                control.RefreshTile();
            }
        }



        protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            IsSelected = !IsSelected;
        }

        private static void OnIsSelectedPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void RefreshTile()
        {
            var geom = new System.Windows.Shapes.Path
            {
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Tile.Color)),
                Stroke = new SolidColorBrush(Colors.Black),
                Stretch = Stretch.Fill,
                IsHitTestVisible = false,
                Data = Converters.FromPathMarkup(Tile.Geometry)
            };
            geom.Fill.Freeze();
            geom.Stroke.Freeze();
            geom.Data.Freeze();

            Height = Tile.Height;
            Width = Tile.Width;
            Content = geom;


            //Canvas.SetTop(this, Tile.Y);
            //Canvas.SetLeft(this, Tile.X);
        }

    }
}
