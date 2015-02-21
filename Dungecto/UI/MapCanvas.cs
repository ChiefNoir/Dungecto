using Dungecto.Model;
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
        /// <summary> <see cref="SelectedItem"/> DependencyProperty </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register
            (
                "SelectedItem",
                typeof(MapTile),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(null)
            );

        //TODO: as binding
        /// <summary> Tile template </summary>
        private ControlTemplate _tileTemplate = Application.Current.FindResource("TileTemplate") as ControlTemplate;

        /// <summary> Remove selected item command </summary>
        private ICommand _removeSelectedItemCommand;

        /// <summary> Create map canvas </summary>
        public MapCanvas()
        {
            Resize(10, 10, 50);
        }

        /// <summary> Remove selected item command </summary>
        public ICommand RemoveSelectedItemCommand
        {
            get
            {
                return _removeSelectedItemCommand ?? (_removeSelectedItemCommand = new BasicCommand(() => Remove(SelectedItem)));
            }
        }
        
        /// <summary> Get selected map tile </summary>
        public MapTile SelectedItem
        {
            get { return (MapTile)GetValue(SelectedItemProperty); }
            private set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary> Add tile to map </summary>
        /// <param name="tile"> Tile to add </param>
        public void Add(MapTile tile)
        {
            if (tile == null) { return; }
            if (Children.Contains(tile)) { return; }

            Children.Add(tile);

            tile.PreviewMouseDown += TilePreviewMouseDown;
        }

        /// <summary> Remove tile from map </summary>
        /// <param name="tile"> Tile to remove </param>
        public void Remove(MapTile tile)
        {
            if (tile == null) { return; }

            Children.Remove(tile);
            tile.PreviewMouseLeftButtonDown -= TilePreviewMouseDown;
        }

        //TODO: here
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

            SelectedItem = null;
        }

        /// <summary>Left click on map. Remove selection from <see cref="SelectedItem"/> </summary>
        /// <param name="e">~</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (SelectedItem != null)
            {
                SelectedItem.IsSelected = false;
                SelectedItem = null;
            }
        }

        /// <summary>If drop data is <see cref="Dungecto.Model.TileDescription"/> in {MapTile} format - add new tile to map </summary>
        /// <param name="e">~</param>
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data == null) { return; }

            var dropData = e.Data.GetData("{MapTile}");
            if (dropData == null) { return; }

            var dropTile = dropData as TileDescription;
            if (dropTile == null) { return; }

            Add(new MapTile(dropTile, e.GetPosition(this), _tileTemplate));
        }

        /// <summary> Drag enters the map </summary>
        /// <param name="e">~</param>
        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            e.Effects = DragDropEffects.Copy;
        }

        /// <summary> Event left click on map tile, changing <see cref="SelectedItem"/> to event sender</summary>
        /// <param name="sender"><seealso cref="MapTile"/></param>
        /// <param name="e">~</param>
        private void TilePreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsSelected = false;
                SelectedItem = null;
            }

            SelectedItem = sender as MapTile;

            if (SelectedItem != null)
            {
                SelectedItem.IsSelected = true;
            }
        }

    }
}