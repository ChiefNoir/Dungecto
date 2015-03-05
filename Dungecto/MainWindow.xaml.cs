using System.Windows;
using Dungecto.ViewModel;
using MahApps.Metro.Controls;
using System.Windows.Input;
using System.Windows.Controls;
using Dungecto.View;
using Dungecto.Common;

namespace Dungecto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow 
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }



        private void ShowHideMainMenu(object sender, RoutedEventArgs e)
        {
            MainMenu.IsOpen = !MainMenu.IsOpen;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var contex = (sender as ContentControl).DataContext;
            if (contex == null) { return; }

            var desc = contex as Dungecto.Model.Tile;
            if (desc == null) { return; }



            var dragObj = new DataObject("{MapTile}", desc.Clone());
            DragDrop.DoDragDrop(this, dragObj, DragDropEffects.Copy);
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            if (e == null || e.Data == null) { return; }

            var dropData = e.Data.GetData("{MapTile}");
            if (dropData == null) { return; }

            var dropTile = dropData as Dungecto.Model.Tile;
            if (dropTile == null) { return; }


            dropTile.X = e.GetPosition(sender as UIElement).X;
            dropTile.Y = e.GetPosition(sender as UIElement).Y;

            //HACK
            (DataContext as MainViewModel).Map.Tiles.Add(dropTile);
        }


        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

        }

        MapCanvas ss;
        private void ThumbListStack_Loaded(object sender, RoutedEventArgs e)
        {
            ss = sender as MapCanvas;
        }

        private void ExportMap(object sender, RoutedEventArgs e)
        {
            if (ss == null) { return; }

            var file = Dialogs.ShowSaveDialog("", ".png");

            if (!string.IsNullOrEmpty(file))
            {
                Exporter.ToPng(ss, file);
            }
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
