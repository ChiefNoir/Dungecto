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
                    Width = 50,
                    Height = 50
                };

                //var menu = new MenuItem
                //{
                //    Icon = geom,
                //    Header = item.Name,
                //    Tag = item,
                //};
                //menu.Click += menu_Click;

                //MenuAdd.Items.Add(menu);
                itms.Items.Add(item);
            }
        }

        void menu_Click(object sender, RoutedEventArgs e)
        {
            var des = (sender as MenuItem).Tag as TileDescription;
            var templ = Application.Current.FindResource("DesignerItemTemplate") as ControlTemplate;
            var tile = new MapTile(des, MapCanvas.LastMousePosition, templ);

            MapCanvas.Add(tile);

        }

        private void Expander_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.dragStartPoint = new Point?(e.GetPosition(this));
            slnd = sender;
        }

        object slnd;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                this.dragStartPoint = null;
            }

            if (this.dragStartPoint.HasValue)
            {
                Point position = e.GetPosition(this);
                if ((SystemParameters.MinimumHorizontalDragDistance <=
                    Math.Abs((double)(position.X - this.dragStartPoint.Value.X))) ||
                    (SystemParameters.MinimumVerticalDragDistance <=
                    Math.Abs((double)(position.Y - this.dragStartPoint.Value.Y))))
                {
                    //string xamlString = XamlWriter.Save(this.Content);
                    DataObject dataObject = new DataObject("DESIGNER_ITEM", (slnd as ListBox).SelectedItem) ;

                    if (dataObject != null)
                    {
                        DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);
                    }
                }

                e.Handled = true;
            }
        }


        private Point? dragStartPoint = null;

        private void MapCanvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void MapCanvas_Drop(object sender, DragEventArgs e)
        {
            var ss = e.Data.GetData("DESIGNER_ITEM");
            var templ = Application.Current.FindResource("DesignerItemTemplate") as ControlTemplate;
            MapCanvas.Add(new MapTile(ss as TileDescription, e.GetPosition(sender as MapCanvas), templ));
        }

    }
}
