using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dungecto.UI
{
    /// <summary> Map canvas </summary>
    public class MapCanvas : Canvas
    {
        /// <summary> <see cref="SelectedTile"/> DependencyProperty </summary>
        public static readonly DependencyProperty SelectedTileProperty = DependencyProperty.Register
            (
                "SelectedTile",
                typeof(MapTile),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(null)
            );

        public Point LastMousePosition { get; set; }

        public MapCanvas():base()
        {
            Resize(10, 10, 50);
        }

        /// <summary> Get selected map tile </summary>
        public MapTile SelectedTile
        {
            get { return (MapTile)GetValue(SelectedTileProperty); }
            private set { SetValue(SelectedTileProperty, value); }
        }

        /// <summary> Add tile to map </summary>
        /// <param name="tile"> Tile to add </param>
        public void Add(MapTile tile)
        {
            if (tile == null) { return; }
            if (Children.Contains(tile) ) { return; }

            Children.Add(tile);

            tile.PreviewMouseLeftButtonDown += TilePreviewMouseLeftButtonDown;
        }

        /// <summary> Remove tile from map </summary>
        /// <param name="tile"> Tile to remove </param>
        public void Remove(MapTile tile)
        {
            Children.Remove(tile);
            tile.PreviewMouseLeftButtonDown -= TilePreviewMouseLeftButtonDown;
        }

        /// <summary> Remove selected tile </summary>
        public void RemoveSelected()
        {
            Remove(SelectedTile);
        }

        /// <summary>Left click on map. Remove selection from <see cref="SelectedTile"/> </summary>
        /// <param name="e">~</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (SelectedTile != null)
            {
                SelectedTile.IsSelected = false;
                SelectedTile = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            LastMousePosition = e.GetPosition(this);
        }

        /// <summary> Event left click on map tile, changing <see cref="SelectedTile"/> to event sender</summary>
        /// <param name="sender"><seealso cref="MapTile"/></param>
        /// <param name="e">~</param>
        private void TilePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedTile != null)
            {
                SelectedTile.IsSelected = false;
                SelectedTile = null;
            }

            SelectedTile = sender as MapTile;

            if (SelectedTile != null)
            {
                SelectedTile.IsSelected = true;
            }
        }


        public void Resize(int columns, int rows, int blockSize)
        {

            Width = columns * blockSize;
            Height = rows * blockSize;

            var brush = new VisualBrush
            {
                Viewport = new Rect(0, 0, blockSize, blockSize),
                Viewbox = new Rect(0, 0, blockSize, blockSize),
                ViewboxUnits = BrushMappingMode.Absolute,
                ViewportUnits = BrushMappingMode.Absolute,
                TileMode = TileMode.Tile,
                Visual =
                    new Rectangle
                    {
                        Stroke = new SolidColorBrush(Colors.Gray),
                        Height = blockSize,
                        Width = blockSize,
                        StrokeThickness = .5
                    }
            };

            Background = brush;


            SelectedTile = null;
        }
    }
}
