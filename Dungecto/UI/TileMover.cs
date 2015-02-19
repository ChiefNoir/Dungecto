using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Dungecto.UI
{
    /// <summary> Tile mover (thing over tile allowing to move tile with mouse) </summary>
    /// <remarks> Using in <code>Templates\TileTemplate.xaml</code> </remarks>
    public class TileMover : Thumb
    {
        /// <summary> Create tile mover </summary>
        public TileMover()
        {
            DragDelta += MoveThumbDragDelta;
        }

        /// <summary> Destroy tile mover </summary>
        ~TileMover()
        {
            DragDelta -= MoveThumbDragDelta;
        }

        /// <summary> Move tile </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void MoveThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            var item = DataContext as Control;
            if (item == null) { return; }

            var parent = item.Parent as Panel;
            if (parent == null) 
            {
                Canvas.SetLeft(item, Canvas.GetLeft(item) + e.HorizontalChange);
                Canvas.SetTop(item, Canvas.GetTop(item) + e.VerticalChange);
            }

            //check on moving beyond parent's borders
            var left = Canvas.GetLeft(item) + e.HorizontalChange;
            if (left < 0)
            {
                left = 0;
            }
            if (left + item.ActualWidth > parent.Width)
            {
                left = parent.Width - item.ActualWidth;
            }


            var top = Canvas.GetTop(item) + e.VerticalChange;
            if (top < 0)
            {
                top = 0;
            }
            if (top + item.ActualHeight > parent.Height)
            {
                top = parent.Height - item.ActualHeight;
            }

            Canvas.SetLeft(item, left);
            Canvas.SetTop(item, top);
        }
    }
}
