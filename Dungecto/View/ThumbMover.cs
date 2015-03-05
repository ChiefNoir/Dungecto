using System.Windows;
using System.Windows.Controls.Primitives;

namespace Dungecto.View
{
    /// <summary> Thumb mover (allows to move thing with mouse) </summary>
    public class ThumbMover : Thumb
    {
        /// <summary> <see cref="X"/> DependencyProperty </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(ThumbMover) );

        /// <summary> <see cref="Y"/> DependencyProperty </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(ThumbMover) );

        /// <summary> Get/Set Y position </summary>
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        /// <summary> Get/Set X position </summary>
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty MinYProperty = DependencyProperty.Register("MinY", typeof(double), typeof(ThumbMover));

        public double MinY
        {
            get { return (double)GetValue(MinYProperty); }
            set { SetValue(MinYProperty, value); }
        }

        public static readonly DependencyProperty MinXProperty = DependencyProperty.Register("MinX", typeof(double), typeof(ThumbMover));

        public double MinX
        {
            get { return (double)GetValue(MinXProperty); }
            set { SetValue(MinXProperty, value); }
        }

        /// <summary> Create tile mover </summary>
        public ThumbMover()
        {
            DragDelta += MoveThumbDragDelta;
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
            if (X + e.HorizontalChange >= MinX)
            {
                X += e.HorizontalChange;
            }
            else
            {
                X = MinX;
            }

            if (Y + e.VerticalChange >= MinY)
            {
                Y += e.VerticalChange;
            }
            else
            {
                Y = MinY;
            }
        }

    }
}

