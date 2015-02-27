using Dungecto.Common;
using Dungecto.Model;
using Dungecto.UI;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dungecto
{
    /// <summary> Main editor window </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary> Preset tiles </summary>
        public ObservableCollection<Dungecto.Model.Tile> Tiles { get; private set; }

        /// <summary> Create main editor window </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Tiles = Serializer.FromXml<ObservableCollection<Dungecto.Model.Tile>>("Config/Tiles.xml");
        }

        /// <summary>Click on item in <see cref="Tiles"/> List on UI</summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var contex = (sender as ContentControl).DataContext;
            if (contex == null) { return; }

            var desc = contex as Dungecto.Model.Tile;
            if (desc == null) { return; }

            var dragObj = new DataObject("{MapTile}", desc);
            DragDrop.DoDragDrop(this, dragObj, DragDropEffects.Copy);
        }

        /// <summary> Show map properties </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void ShowMapProperties(object sender, RoutedEventArgs e)
        {
            new MapPropertiesWindow().ShowDialog(MapCanvas);
        }


        private void ShowHideMainMenu(object sender, RoutedEventArgs e)
        {
            MainMenu.IsOpen = !MainMenu.IsOpen;
        }



        private void CreateNewMap(object sender, RoutedEventArgs e)
        {
            MapCanvas.Clear();
        }

        private void SaveMap(object sender, RoutedEventArgs e)
        {
            var path = Dialogs.ShowSaveDialog(Properties.Resources.Save, ".xml");

            if (path != null)
            {
                Serializer.ToXml<Map>(MapCanvas.GetDescription(), path);
            }
        }

        private void ExportMap(object sender, RoutedEventArgs e)
        {
            var path = Dialogs.ShowSaveDialog(Properties.Resources.Export, ".png");

            if (path != null)
            {
                MapCanvas.SelectedItem = null;
                Exporter.ToPng(MapCanvas, path);
            }
        }

        private void OpenMap(object sender, RoutedEventArgs e)
        {
            var path = Dialogs.ShowOpenDialog(Properties.Resources.Open, ".xml");

            if (path != null)
            {
                var map = Serializer.FromXml<Map>(path);

                MapCanvas.Load(map);
            }
        }

    }
}
