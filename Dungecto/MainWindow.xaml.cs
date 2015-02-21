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

        /// <summary> Selection changed in list of tiles descriptions. Begin drag&drop</summary>
        /// <param name="sender">ListView</param>
        /// <param name="e">~</param>
        private void TileDescriptionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listview = (sender as ListView);
            if (listview == null) { return; }

            var desc = listview.SelectedItem as TileDescription;
            if (desc == null) { return; }

            var dragObj = new DataObject("{MapTile}", desc);
            DragDrop.DoDragDrop(this, dragObj, DragDropEffects.Copy);
        }

    }
}