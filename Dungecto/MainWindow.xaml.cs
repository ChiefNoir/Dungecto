using Dungecto.Common;
using Dungecto.Model;
using Dungecto.UI;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dungecto
{
    /// <summary> Main editor window </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Drag start position </summary>
        private Point? _dragStartPoint = null;

        private ControlTemplate _tileTemplate = Application.Current.FindResource("TileTemplate") as ControlTemplate;

        /// <summary> Selected tile on map </summary>
        public TileDescription SelectedTileDescription { get; set; }

        /// <summary> Preset tiles </summary>
        public ObservableCollection<TileDescription> Tiles { get; set; }

        /// <summary> Create main editor window </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Tiles = Serializer.FromXml<ObservableCollection<TileDescription>>("Config/Tiles.conf");
        }

        /// <summary> Mouse move over the window </summary>
        /// <param name="e">~</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (SelectedTileDescription == null) { return; }
            if (e.LeftButton != MouseButtonState.Pressed) { _dragStartPoint = null; return; }
            if (_dragStartPoint == null) { return; }


            var dragObj = new DataObject("{MapTile}", SelectedTileDescription);

            DragDrop.DoDragDrop(this, dragObj, DragDropEffects.Copy);

            e.Handled = true;
        }

        /// <summary> Click on tile in tile container </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void TileContainer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = null;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _dragStartPoint = e.GetPosition(this);
            }
        }

        /// <summary> Drag enters Canvas </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void MapCanvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        /// <summary>Drop on Canvas</summary>
        /// <param name="sender"> <code>System.Windows.Controls.Canvas</code> </param>
        /// <param name="e">~</param>
        private void MapCanvas_Drop(object sender, DragEventArgs e)
        {
            var dropData = e.Data.GetData("{MapTile}");

            if (dropData != null)
            {
                MapCanvas.Add(new MapTile(SelectedTileDescription, e.GetPosition(sender as MapCanvas), _tileTemplate));
            }

            _dragStartPoint = null;
        }

        /// <summary> Click on "Remove" menu. Removes selected tile from map </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void MenuRemove_Click(object sender, RoutedEventArgs e)
        {
            MapCanvas.Remove(MapCanvas.SelectedItem);
        }
    }
}