using System.Windows;
using System.Windows.Controls.Primitives;

namespace Dungecto.View
{
    /// <summary> Thumb mover (allows to move thing with mouse) </summary>
    class ThumbMover : Thumb
    {
        /// <summary> <see cref="MinX"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinXProperty = DependencyProperty.Register("MinX", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="MaxX"/> DependencyProperty </summary>
        public static readonly DependencyProperty MaxXProperty = DependencyProperty.Register("MaxX", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="MinY"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinYProperty = DependencyProperty.Register("MinY", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="MaxY"/> DependencyProperty </summary>
        public static readonly DependencyProperty MaxYProperty = DependencyProperty.Register("MaxY", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="X"/> DependencyProperty </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="Y"/> DependencyProperty </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="ContentWidth"/> DependencyProperty </summary>
        public static readonly DependencyProperty ContentWidthProperty = DependencyProperty.Register("ContentWidth", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="ContentHeight"/> DependencyProperty </summary>
        public static readonly DependencyProperty ContentHeightProperty = DependencyProperty.Register("ContentHeight", typeof(double), typeof(ThumbMover));

        /// <summary> Create tile mover </summary>
        public ThumbMover()
        {
            DragDelta += MoveThumbDragDelta;
        }

        /// <summary> Get/set min value for <see cref="MinX"/> property </summary>
        public double MinX
        {
            get { return (double)GetValue(MinXProperty); }
            set { SetValue(MinXProperty, value); }
        }

        /// <summary> Get/set min value for <see cref="MaxX"/> property </summary>
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

        /// <summary> Get/set min value for <see cref="MaxY"/> property </summary>
        public double MaxY
        {
            get { return (double)GetValue(MaxYProperty); }
            set { SetValue(MaxYProperty, value); }
        }

        /// <summary> Get/set Width of the content wich will be moved</summary>
        public double ContentWidth
        {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }

        /// <summary> Get/set Height of the content wich will be moved</summary>
        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }

        /// <summary> Get/set X position </summary>
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set 
            {
                //NOTE: Important part
                if (value >= MinX && value + ContentWidth - MinX <= MaxX)
                {
                    SetValue(XProperty, value);
                }
            }
        }

        /// <summary> Get/set Y position </summary>
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set
            {
                //NOTE: Important part
                if (value >= MinY && value + ContentHeight - MinY <= MaxY)
                {
                    SetValue(YProperty, value);
                }
            }
        }
        
        /// <summary> Destroy tile mover </summary>
        ~ThumbMover()
        {
            DragDelta -= MoveThumbDragDelta;
        }

        /// <summary> Move tile </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void MoveThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            X += e.HorizontalChange;
            Y += e.VerticalChange;
        }
    }
}