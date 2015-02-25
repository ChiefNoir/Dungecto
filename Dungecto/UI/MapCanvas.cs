using Dungecto.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Markup;

namespace Dungecto.UI
{
    /// <summary> Map canvas </summary>
    public class MapCanvas : Canvas, INotifyPropertyChanged
    {
        /// <summary> <see cref="SelectedItem"/> DependencyProperty </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register
            (
                "SelectedItem",
                typeof(MapTile),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(null)
            );

        /// <summary> Columns in map </summary>
        private int _columns = 10;

        /// <summary> Last columns value after resize</summary>
        private int _columnsLastValue;

        /// <summary> Number of rows </summary>
        private int _rows = 10;

        /// <summary> Last rows value after resize </summary>
        private int _rowsLastValue;

        /// <summary> Sector height </summary>
        private int _sectorHeight = 50;

        /// <summary> Last sector height after resize </summary>
        private int _sectorHeightLastValue;

        /// <summary> Sector width </summary>
        private int _sectorWidth = 50;

        /// <summary> Last sector width after resize </summary>
        private int _sectorWidthLastValue;

        //TODO: as binding
        /// <summary> Tile template </summary>
        private ControlTemplate _tileTemplate = Application.Current.FindResource("TileTemplate") as ControlTemplate;

        /// <summary> Resize map </summary>
        private ICommand _undoResizeMapPreparationsCommand;

        /// <summary> Remove selected item command </summary>
        private ICommand _removeSelectedItemCommand;

        /// <summary> Resize map </summary>
        private ICommand _resizeMapCommand;

        /// <summary> Create map canvas </summary>
        public MapCanvas()
        {
            Resize();
        }

        /// <summary> PropertyChanged event </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary> Get/set numbers of columns </summary>
        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Columns"));
            }
        }

        /// <summary> Get/set numbers of rows </summary>
        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Rows"));
            }
        }

        /// <summary> Get/set sector height </summary>
        public int SectorHeight
        {
            get { return _sectorHeight; }
            set
            {
                _sectorHeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SectorHeight"));
            }
        }

        /// <summary> Get/set sector width </summary>
        public int SectorWidth
        {
            get { return _sectorWidth; }
            set
            {
                _sectorWidth = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SectorWidth"));
            }
        }

        /// <summary> Get selected map tile </summary>
        public MapTile SelectedItem
        {
            get { return (MapTile)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary> Undo resize map preparations </summary>
        public ICommand UndoResizeMapPreparationsCommand
        {
            get
            {
                return _undoResizeMapPreparationsCommand ?? (_undoResizeMapPreparationsCommand = new BasicCommand(() => UndoResizeMapPreparations()));
            }
        }

        /// <summary> Remove selected item command </summary>
        public ICommand RemoveSelectedItemCommand
        {
            get
            {
                return _removeSelectedItemCommand ?? (_removeSelectedItemCommand = new BasicCommand(() => Remove(SelectedItem)));
            }
        }

        /// <summary> Resize command </summary>
        public ICommand ResizeMapCommand
        {
            get
            {
                return _resizeMapCommand ?? (_resizeMapCommand = new BasicCommand(() => Resize()));
            }
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

        /// <summary> Drag enters the map </summary>
        /// <param name="e">~</param>
        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            e.Effects = DragDropEffects.Copy;
        }

        /// <summary>If drop data is <see cref="Dungecto.Model.Tile"/> in {MapTile} format - add new tile to map </summary>
        /// <param name="e">~</param>
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data == null) { return; }

            var dropData = e.Data.GetData("{MapTile}");
            if (dropData == null) { return; }

            var dropTile = dropData as Tile;
            if (dropTile == null) { return; }

            Add(new MapTile(dropTile, e.GetPosition(this), _tileTemplate));
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

        /// <summary> Resize map </summary>
        private void Resize()
        {
            _sectorWidthLastValue = SectorWidth;
            _sectorHeightLastValue = SectorHeight;
            _rowsLastValue = Rows;
            _columnsLastValue = Columns;

            Width = Columns * SectorWidth;
            Height = Rows * SectorHeight;

            var brush = new VisualBrush
            {
                Viewport = new Rect(0, 0, SectorWidth, SectorHeight),
                Viewbox = new Rect(0, 0, SectorWidth, SectorHeight),
                ViewboxUnits = BrushMappingMode.Absolute,
                ViewportUnits = BrushMappingMode.Absolute,
                TileMode = TileMode.Tile,
                Visual = new Rectangle
                        {
                            Stroke = new SolidColorBrush(Colors.Gray),
                            Height = SectorHeight,
                            Width = SectorWidth,
                            Fill = new SolidColorBrush(Colors.White),
                            StrokeThickness = .5
                        }
            };

            Background = brush;

            SelectedItem = null;
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

        /// <summary> Undo changes in <see cref="SectorWidth"/>, <see cref="SectorHeight"/>, <see cref="Rows"/>, <see cref="Columns"/> </summary>
        private void UndoResizeMapPreparations()
        {
            SectorWidth = _sectorWidthLastValue;
            SectorHeight = _sectorHeightLastValue;
            Rows = _rowsLastValue;
            Columns = _columnsLastValue;
        }

        /// <summary> Get map descrition </summary>
        /// <returns>Map description</returns>
        public Map GetDescription()
        {
            var map = new Map
            {
                Columns = Columns,
                Rows = Rows,
                SectorHeight = SectorHeight,
                SectorWidth = SectorWidth
            };

            foreach (MapTile item in Children)
            {
                var geom = item.Content as System.Windows.Shapes.Path;

                var bgBrush = geom.Fill as SolidColorBrush;

                var ss = XamlWriter.Save(geom.Data);

                map.Tiles.Add(new Tile()
                {
                    GeometryPath = ss,
                    Height = item.Height,
                    HexColor = bgBrush == null ? "#bf7e00": bgBrush.Color.ToString(),
                    Position = new Point
                        (
                           Convert.ToInt32(Canvas.GetLeft(item)), 
                           Convert.ToInt32(Canvas.GetTop(item))
                        ),
                    Width = item.Width
                });
            }

            return map;
        }



        public void Clear()
        {
            SelectedItem = null;

            foreach (MapTile tile in Children)
            {
                tile.PreviewMouseLeftButtonDown -= TilePreviewMouseDown;
            }

            Children.Clear();


            Columns = 10;
            Rows = 10;
            SectorHeight = 50;
            SectorWidth = 50;
            Resize();
        }

        public void Load(Map map)
        {
            Clear();

            Columns = map.Columns;
            Rows = map.Rows;
            SectorHeight = map.SectorHeight;
            SectorWidth = map.SectorWidth;

            Resize();
            foreach (var tile in map.Tiles.Where(x=>x.Position.HasValue))
            {
                Add(new MapTile(tile, tile.Position.Value, _tileTemplate));
            }
        }
    }
}