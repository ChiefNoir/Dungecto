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
        /// <summary> Preset tiles </summary>
        public ObservableCollection<TileDescription> Tiles { get; set; }

        /// <summary> Create main editor window </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Tiles = Serializer.FromXml<ObservableCollection<TileDescription>>("Config/Tiles.conf");
        }

        /// <summary> Click on "Remove" menu. Removes selected tile from map </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void MenuRemove_Click(object sender, RoutedEventArgs e)
        {
            MapCanvas.Remove(MapCanvas.SelectedItem);
        }

        /// <summary>Click on item in <see cref="Tiles"/> List on UI</summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var contex = (sender as ContentControl).DataContext;
            if (contex == null) { return; }

            var desc = contex as TileDescription;
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
    }
}
