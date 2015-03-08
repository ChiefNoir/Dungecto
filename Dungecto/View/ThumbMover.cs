using System.Windows;
using System.Windows.Controls.Primitives;

namespace Dungecto.View
{
    /// <summary> Thumb mover (allows to move thing with mouse) </summary>
    class ThumbMover : Thumb
    {
        /// <summary> <see cref="MinX"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinXProperty = DependencyProperty.Register("MinX", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="MinY"/> DependencyProperty </summary>
        public static readonly DependencyProperty MinYProperty = DependencyProperty.Register("MinY", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="X"/> DependencyProperty </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(ThumbMover));

        /// <summary> <see cref="Y"/> DependencyProperty </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(ThumbMover));

        /// <summary> Create tile mover </summary>
        public ThumbMover()
        {
            DragDelta += MoveThumbDragDelta;
        }

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