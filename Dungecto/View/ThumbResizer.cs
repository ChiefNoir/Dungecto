using System.Windows;
using System.Windows.Controls.Primitives;

namespace Dungecto.View
{
    /// <summary> Thumb mover (things in the tile's corners, allowing to resize tile with mouse) </summary>
    class ThumbResizer : Thumb
    {
        /// <summary> <see cref="MinX"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinXProperty = DependencyProperty.Register("MinX", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="MinY"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinYProperty = DependencyProperty.Register("MinY", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="X"/> DependencyProperty </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="Y"/> DependencyProperty </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(ThumbResizer));

        //TODO: "ParentWidth" - name doesn't looks nice
        /// <summary> <see cref="ParentWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty ParentWidthProperty = DependencyProperty.Register("ParentWidth", typeof(double), typeof(ThumbResizer));

        //TODO: "ParentHeight" - name doesn't looks nice
        /// <summary> <see cref="ParentHeight"/> DependencyProperty </summary>
        public static readonly DependencyProperty ParentHeightProperty = DependencyProperty.Register("ParentHeight", typeof(double), typeof(ThumbResizer));

        /// <summary> Get/set min value for <see cref="X"/> property </summary>
        public double MinX
        {
            get { return (double)GetValue(MinXProperty); }
            set { SetValue(MinXProperty, value); }
        }

        /// <summary> Get/set min value for <see cref="Y"/> property </summary>
        public double MinY
        {
            get { return (double)GetValue(MinYProperty); }
            set { SetValue(MinYProperty, value); }
        }

        /// <summary> Get/set X position </summary>
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set
            {
                if (value >= MinX)
                {
                    SetValue(XProperty, value);
                }
                else
                {
                    SetValue(XProperty, MinX);
                }
            }
        }

        /// <summary> Get/set Y position </summary>
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set
            {
                if (value >= MinY)
                {
                    SetValue(YProperty, value);
                }
                else
                {
                    SetValue(YProperty, MinY);
                }
            }
        }

        /// <summary> Get/set width </summary>
        public double ParentWidth
        {
            get { return (double)GetValue(ParentWidthProperty); }
            set { SetValue(ParentWidthProperty, value); }
        }

        /// <summary> Get/set height </summary>
        public double ParentHeight
        {
            get { return (double)GetValue(ParentHeightProperty); }
            set { SetValue(ParentHeightProperty, value); }
        }

        /// <summary> Initializes a new instance of the TileResizer class </summary>
        public ThumbResizer()
        {
            DragDelta += MoveThumbDragDelta;
        }

        /// <summary> Destroy tile resizer </summary>
        ~ThumbResizer()
        {
            DragDelta -= MoveThumbDragDelta;
        }

        /// <summary> Resize tile with mouse </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void MoveThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            //TODO: checks on resize out of parent borders
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    {
                        if (ParentHeight + e.VerticalChange > MinHeight)
                        {
                            ParentHeight += e.VerticalChange;
                        }
                        break;
                    }
                case VerticalAlignment.Top:
                    {
                        if (Y + e.VerticalChange >= 0)
                        {
                            Y += e.VerticalChange;
                            ParentHeight -= e.VerticalChange;
                        }
                        break;
                    }
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        if (X + e.HorizontalChange > 0)
                        {
                            X += e.HorizontalChange;
                            ParentWidth -= e.HorizontalChange;
                        }
                        break;
                    }
                case HorizontalAlignment.Right:
                    {
                        if (ParentWidth + e.HorizontalChange > MinWidth)
                        {
                            ParentWidth += e.HorizontalChange;
                        }
                        break;
                    }
            }

            e.Handled = true;
        }

    }
}
