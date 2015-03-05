using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dungecto.View
{
    class MapCanvas : Canvas
    {
        /// <summary> <see cref="SectorWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty SectorWidthProperty = DependencyProperty.Register
            (
                "SectorWidth",
                typeof(int),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(50)
            );

        /// <summary> Get/set map sector width </summary>
        public int SectorWidth
        {
            get { return (int)GetValue(SectorWidthProperty); }
            set { SetValue(SectorWidthProperty, value); }
        }

        /// <summary> <see cref="SectorWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty SectorHeightProperty = DependencyProperty.Register
            (
                "SectorHeight",
                typeof(int),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(50)
            );

        /// <summary> Get/set map sector width </summary>
        public int SectorHeight
        {
            get { return (int)GetValue(SectorHeightProperty); }
            set { SetValue(SectorHeightProperty, value); }
        }

        /// <summary> <see cref="SectorWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register
            (
                "Columns",
                typeof(int),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(10)
            );

        /// <summary> Get/set map sector width </summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary> <see cref="SectorWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register
            (
                "Rows",
                typeof(int),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(10)
            );

        /// <summary> Get/set map sector width </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public MapCanvas()
        {
            Resize();
        }

        /// <summary> Resize map </summary>
        private void Resize()
        {
            Width = Columns * SectorWidth;
            Height = Rows * SectorHeight;

            var brush = new VisualBrush
            {
                Viewport = new Rect(0, 0, SectorWidth, SectorHeight),
                Viewbox = new Rect(0, 0, SectorWidth, SectorHeight),
                ViewboxUnits = BrushMappingMode.Absolute,
                ViewportUnits = BrushMappingMode.Absolute,
                TileMode = TileMode.Tile,
                Visual = new Rectangle
                {
                    Stroke = new SolidColorBrush(Colors.Gray),
                    Height = SectorHeight,
                    Width = SectorWidth,
                    Fill = new SolidColorBrush(Colors.LightBlue),
                    StrokeThickness = .5
                }
            };

            Background = null;
            Background = brush;
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == "Rows" || e.Property.Name == "Columns" || e.Property.Name == "SectorHeight" || e.Property.Name == "SectorWidth")
            {
                Resize();
            }
        }



    }
}
