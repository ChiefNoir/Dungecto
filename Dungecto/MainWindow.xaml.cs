using Dungecto.Common;
using Dungecto.View;
using Dungecto.ViewModel;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;

namespace Dungecto
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : MetroWindow 
    {
        /// <summary> Initializes a new instance of the MainWindow class. </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        /// <summary> Open if closed/close if open main menu</summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void OpenCloseMainMenu(object sender, RoutedEventArgs e)
        {
            MainMenuFlyout.IsOpen = !MainMenuFlyout.IsOpen;
        }

        /// <summary> Open if closed/close if open map properties</summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void OpenCloseMapProperties(object sender, RoutedEventArgs e)
        {
            MapPropertiesFlyout.IsOpen = !MapPropertiesFlyout.IsOpen;
        }


//TODO: fix drag&drop

        /// <summary>Left click on toolbox. Begin dragging item from toolbox</summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void ToolboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var contex = (sender as FrameworkElement).DataContext;
            if (contex == null) { return; }

            var desc = contex as Model.Tile;
            if (desc == null) { return; }

            var dragObj = new DataObject("{MapTile}", desc.Clone());
            DragDrop.DoDragDrop(this, dragObj, DragDropEffects.Copy);
        }

        /// <summary> Drop on  canvas </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void CanvasDrop(object sender, DragEventArgs e)
        {
            if (e == null || e.Data == null) { return; }

            var dropData = e.Data.GetData("{MapTile}");
            if (dropData == null) { return; }

            var dropTile = dropData as Model.Tile;
            if (dropTile == null) { return; }

            dropTile.X = e.GetPosition(sender as UIElement).X;
            dropTile.Y = e.GetPosition(sender as UIElement).Y;

            //TODO: fix it!
            (DataContext as MainViewModel).Map.Tiles.Add(dropTile);
        }


//TODO: fix saving

        /// <summary> Canvas with tiles </summary>
        /// <remarks> I need this to export tiles to image file</remarks>
        private MapCanvas _canvas = null;
        
        /// <summary> After listview load </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewTemplateLoaded(object sender, RoutedEventArgs e)
        {
            _canvas = sender as MapCanvas;
        }

        /// <summary> Export <see cref="_canvas"/> to png</summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void ExportMap(object sender, RoutedEventArgs e)
        {
            if (_canvas == null) { return; }

            var file = Dialogs.ShowSaveDialog("", ".png");

            if (!string.IsNullOrEmpty(file))
            {
                Exporter.ToPng(_canvas, file);
            }
        }

    }
}
