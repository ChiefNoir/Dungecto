using Dungecto.UI;
using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Dungecto
{
    /// <summary> Window with map properties </summary>
    public partial class MapPropertiesWindow : MetroWindow
    {
        /// <summary> Create with map properties </summary>
        public MapPropertiesWindow()
        {
            InitializeComponent();
        }

        /// <summary> Show dialog </summary>
        /// <param name="map">Map</param>
        /// <remarks>Dialog result is useless. Everything is in MapCanvas class</remarks>
        public void ShowDialog(MapCanvas map)
        {
            DataContext = map;
            ShowDialog();
        }

        /// <summary>Close window</summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }        

        /// <summary>On KeyDown Esc close window</summary>
        /// <param name="e">~</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e != null && e.Key == Key.Escape)
            {
                Close();
            }
        }

        //TODO: fix it
        /// <summary>On close window</summary>
        /// <remarks>Workaround. When window closing execute UndoResizeMapPreparationsCommand</remarks>
        /// <param name="e">~</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (ButtonClose.Command != null)
            {
                ButtonClose.Command.Execute(null);
            }

            base.OnClosing(e);
        }
    }
}