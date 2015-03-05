using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Dungecto.View
{
    using Dungecto.Model;

    /// <summary> Tile mover (things in the tile's corners, allowing to resize tile with mouse) </summary>
    public class TileResizer : Thumb
    {
        private int _min = 10;

        /// <summary> Create tile resizer </summary>
        public TileResizer()
        {
            DragDelta += MoveThumbDragDelta;
        }

        /// <summary> Destroy tile resizer </summary>
        ~TileResizer()
        {
            var ss = this;
            DragDelta -= MoveThumbDragDelta;
        }

        /// <summary> Resize tile with mouse </summary>
        /// <param name="sender">~</param>
        /// <param name="e">~</param>
        private void MoveThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            var designerItem = this.DataContext as Tile;

            if (designerItem == null) { return; }


            //TODO: checks on resize out of parent borders
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    {
                        if (designerItem.Height + e.VerticalChange > _min)
                        {
                            designerItem.Height += e.VerticalChange;
                        }
                        break;
                    }
                case VerticalAlignment.Top:
                    {
                        if (designerItem.Y + e.VerticalChange >= 0)
                        {
                            designerItem.Y += e.VerticalChange;
                            designerItem.Height -= e.VerticalChange;
                        }
                        break;
                    }
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        if (designerItem.X + e.HorizontalChange > 0)
                        {
                            designerItem.X += e.HorizontalChange;
                            designerItem.Width -= e.HorizontalChange;
                        }
                        break;
                    }
                case HorizontalAlignment.Right:
                    {
                        if (designerItem.Width + e.HorizontalChange > _min)
                        {
                            designerItem.Width += e.HorizontalChange;
                        }
                        break;
                    }
            }

            e.Handled = true;
        }


    }
}
