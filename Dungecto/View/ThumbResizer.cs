using System.Windows;
using System.Windows.Controls.Primitives;

namespace Dungecto.View
{
    /// <summary> Thumb mover (things in the tile's corners, allowing to resize tile with mouse) </summary>
    class ThumbResizer : Thumb
    {
        /// <summary> <see cref="MinX"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinXProperty = DependencyProperty.Register("MinX", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="MaxX"/> DependencyProperty </summary>
        public static readonly DependencyProperty MaxXProperty = DependencyProperty.Register("MaxX", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="MaxY"/> DependencyProperty </summary>
        public static readonly DependencyProperty MaxYProperty = DependencyProperty.Register("MaxY", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="MinY"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinYProperty = DependencyProperty.Register("MinY", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="X"/> DependencyProperty </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="Y"/> DependencyProperty </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="ContentWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty ContentWidthProperty = DependencyProperty.Register("ContentWidth", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="ContentHeight"/> DependencyProperty </summary>
        public static readonly DependencyProperty ContentHeightProperty = DependencyProperty.Register("ContentHeight", typeof(double), typeof(ThumbResizer));

        /// <summary> <see cref="MinContent"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinContentProperty = DependencyProperty.Register("MinContent", typeof(int), typeof(ThumbResizer));

        /// <summary> Get/set min value for <see cref="X"/> property </summary>
        public double MinX
        {
            get { return (double)GetValue(MinXProperty); }
            set { SetValue(MinXProperty, value); }
        }

        /// <summary> Get/set min value for <see cref="MaxY"/> property </summary>
        public double MaxX
        {
            get { return (double)GetValue(MaxXProperty); }
            set { SetValue(MaxXProperty, value); }
        }

        /// <summary> Get/set min value for <see cref="Y"/> property </summary>
        public double MinY
        {
            get { return (double)GetValue(MinYProperty); }
            set { SetValue(MinYProperty, value); }
        }

        /// <summary> Get/set min value for <see cref="Y"/> property </summary>
        public int MinContent
        {
            get { return (int)GetValue(MinContentProperty); }
            set { SetValue(MinContentProperty, value); }
        }

        /// <summary> Get/set min value for <see cref="MaxX"/> property </summary>
        public double MaxY
        {
            get { return (double)GetValue(MaxYProperty); }
            set { SetValue(MaxYProperty, value); }
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

        /// <summary> Get/ContentWidth width </summary>
        public double ContentWidth
        {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }

        /// <summary> Get/set height </summary>
        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
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
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    {
                        if (ContentHeight + e.VerticalChange >= MinHeight && ContentHeight + e.VerticalChange + Y <= MaxY && ContentHeight + e.VerticalChange >= MinContent)
                        {
                            ContentHeight += e.VerticalChange;
                        }
                        break;
                    }
                case VerticalAlignment.Top:
                    {
                        if (Y + e.VerticalChange >= MinY && ContentHeight - e.VerticalChange >= MinContent)
                        {
                            Y += e.VerticalChange;
                            ContentHeight -= e.VerticalChange;
                        }
                        break;
                    }
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        if (X + e.HorizontalChange >= MinX && ContentWidth - e.HorizontalChange >= MinContent)
                        {
                            X += e.HorizontalChange;
                            ContentWidth -= e.HorizontalChange;
                        }
                        break;
                    }
                case HorizontalAlignment.Right:
                    {
                        if (ContentWidth + e.HorizontalChange >= MinWidth && ContentWidth + e.HorizontalChange >= MinContent && ContentWidth + e.HorizontalChange + X <= MaxX)
                        {
                            ContentWidth += e.HorizontalChange;
                        }
                        break;
                    }
            }

            e.Handled = true;
        }

    }
}
