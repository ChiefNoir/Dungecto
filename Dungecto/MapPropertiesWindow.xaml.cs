using Dungecto.UI;
using System.Windows;
using System.Windows.Input;

namespace Dungecto
{
    /// <summary> Window with map properties </summary>
    public partial class MapPropertiesWindow : Window
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

            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}