using Dungecto.Common;
using Dungecto.Model;
using Dungecto.UI;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dungecto
{

    //TODO: Clean up & Refactor, still looks bad
    public partial class MainWindow : Window
    {
        private Point? dragStartPoint = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Tiles = Serializer.FromXml<ObservableCollection<TileDescription>>("Config/Tiles.conf");
        }

        public TileDescription SelectedTileDescription { get; set; }

        /// <summary> Preset tiles </summary>
        public ObservableCollection<TileDescription> Tiles { get; set; }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (SelectedTileDescription == null) { return; }
            if (e.LeftButton != MouseButtonState.Pressed) { dragStartPoint = null; return; }
            if (dragStartPoint == null) { return; }


            var dragObj = new DataObject("MapTile", SelectedTileDescription);

            DragDrop.DoDragDrop(this, dragObj, DragDropEffects.Copy);

            e.Handled = true;
        }

        private void Expander_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            dragStartPoint = null;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                dragStartPoint = new Point?(e.GetPosition(this));
            }
        }
        private void MapCanvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void MapCanvas_Drop(object sender, DragEventArgs e)
        {
            var ss = e.Data.GetData("MapTile");
            var templ = Application.Current.FindResource("TileTemplate") as ControlTemplate;
            MapCanvas.Add(new MapTile(SelectedTileDescription, e.GetPosition(sender as MapCanvas), templ));

            dragStartPoint = null;
        }
    }
}