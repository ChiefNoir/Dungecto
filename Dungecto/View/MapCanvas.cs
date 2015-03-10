using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dungecto.View
{
    /// <summary> Map canvas </summary>
    class MapCanvas : Canvas
    {
        /// <summary> <see cref="SectorWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty SectorWidthProperty = DependencyProperty.Register
            (
                "SectorWidth",
                typeof(int),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(50, OnSizeChanged)
            );

        /// <summary> <see cref="SectorWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty SectorHeightProperty = DependencyProperty.Register
            (
                "SectorHeight",
                typeof(int),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(50, OnSizeChanged)
            );

        /// <summary> <see cref="SectorWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register
            (
                "Columns",
                typeof(int),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(10, OnSizeChanged)
            );

        /// <summary> <see cref="SectorWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register
            (
                "Rows",
                typeof(int),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(10, OnSizeChanged)
            );

        /// <summary> <see cref="BackgroundColor"/> DependencyProperty </summary>
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register
            (
                "BackgroundColor",
                typeof(Color),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(Colors.LightBlue, OnSizeChanged)
            );

        /// <summary> Get/set map background color </summary>
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }


        /// <summary> Get/set map sector width </summary>
        public int SectorWidth
        {
            get { return (int)GetValue(SectorWidthProperty); }
            set { SetValue(SectorWidthProperty, value); }
        }

        /// <summary> Get/set map sector width </summary>
        public int SectorHeight
        {
            get { return (int)GetValue(SectorHeightProperty); }
            set { SetValue(SectorHeightProperty, value); }
        }

        /// <summary> Get/set map sector width </summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary> Get/set map sector width </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary> Initializes a new instance of the MapCanvas class </summary>
        public MapCanvas()
        {
            Resize();
        }

        /// <summary> Resize control on sectors and columns changed </summary>
        /// <param name="sender"> <see cref="MapCanvas"/> object </param>
        /// <param name="e">~</param>
        private static void OnSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as MapCanvas;
            if (control == null) { return; }

            control.Resize();
        }

        /// <summary> Recreate map's background and adjust <see cref="base.Width"/> and <see cref="base.Height"/> </summary>
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
                    Fill = new SolidColorBrush(BackgroundColor),
                    StrokeThickness = .5
                }
            };

            Background = null;
            Background = brush;
        }

    }
}
