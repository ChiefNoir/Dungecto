using Dungecto.Common;
using Dungecto.Model;
using Dungecto.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dungecto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TileDescription> _tiles = new List<TileDescription>();


        public MainWindow()
        {
            InitializeComponent();

            _tiles = Serializer.FromXml<List<TileDescription>>("Config/Tiles.conf");
            InitializeMenu(_tiles);
        }




        private void InitializeMenu(List<TileDescription> descrs)
        {
            foreach (var item in descrs)
            {
                var geom = new System.Windows.Shapes.Path
                {
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(item.HexColor)),
                    Stroke = new SolidColorBrush(Colors.Black),
                    Stretch = Stretch.Fill,
                    IsHitTestVisible = false,
                    Data = Geometry.Parse(string.Format(item.GeometryPath, 20, 20)),
                    Width = 20,
                    Height = 20
                };

                var menu = new MenuItem
                {
                    Icon = geom,
                    Header = item.Name,
                    Tag = item,
                };
                menu.Click += menu_Click;

                MenuAdd.Items.Add(menu);
            }
        }

        void menu_Click(object sender, RoutedEventArgs e)
        {
            var des = (sender as MenuItem).Tag as TileDescription;
            var templ = Application.Current.FindResource("DesignerItemTemplate") as ControlTemplate;
            var tile = new MapTile(des, MapCanvas.LastMousePosition, templ);

            MapCanvas.Add(tile);

        }


    }
}
