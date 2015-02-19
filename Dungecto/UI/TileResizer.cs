using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Dungecto.UI
{
    /// <summary> Tile mover (things in the tile's corners, allowing to resize tile with mouse) </summary>
    /// <remarks> Using in <code>Templates\TileTemplate.xaml</code> </remarks>
    public class TileResizer : Thumb
    {
        /// <summary> Create tile resizer </summary>
        public TileResizer()
        {
            DragDelta += MoveThumbDragDelta;
        }

        /// <summary> Destroy tile resizer </summary>
        ~TileResizer()
        {
            DragDelta -= MoveThumbDragDelta;
        }

        /// <summary> Resize tile with mouse </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void MoveThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            Control designerItem = this.DataContext as Control;

            if (designerItem == null) { return; }

            //TODO: checks on resize out of parent borders
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    {
                        var deltaVertical = Math.Min(-e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                        designerItem.Height -= deltaVertical;
                        break;
                    }
                case VerticalAlignment.Top:
                    {
                        var deltaVertical = Math.Min(e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                        Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) + deltaVertical);
                        designerItem.Height -= deltaVertical;
                        break;
                    }
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        var deltaHorizontal = Math.Min(e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);
                        Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) + deltaHorizontal);
                        designerItem.Width -= deltaHorizontal;
                        break;
                    }
                case HorizontalAlignment.Right:
                    {
                        var deltaHorizontal = Math.Min(-e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);
                        designerItem.Width -= deltaHorizontal;
                        break;
                    }
            }

            e.Handled = true;
        }
    }
}
