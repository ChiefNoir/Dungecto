using System.Windows;
using System.Windows.Controls;

namespace Dungecto.UI
{
    /// <summary> Map canvas </summary>
    public class MapCanvas : Canvas
    {
        /// <summary> <see cref="SelectedTile"/> DependencyProperty </summary>
        public static readonly DependencyProperty SelectedTileProperty = DependencyProperty.Register
            (
                "SelectedTile",
                typeof(MapTile),
                typeof(MapCanvas),
                new FrameworkPropertyMetadata(null)
            );

        /// <summary> Get selected map tile </summary>
        public MapTile SelectedTile
        {
            get { return (MapTile)GetValue(SelectedTileProperty); }
            private set { SetValue(SelectedTileProperty, value); }
        }

        /// <summary> Add tile to map </summary>
        /// <param name="tile"> Tile to add </param>
        public void Add(MapTile tile)
        {
            if (tile == null) { return; }
            if (Children.Contains(tile) ) { return; }

            Children.Add(tile);
        }

        /// <summary> Remove tile from map </summary>
        /// <param name="tile"> Tile to remove </param>
        public void Remove(MapTile tile)
        {
            Children.Remove(tile);
        }

        /// <summary> Remove selected tile </summary>
        public void RemoveSelected()
        {
            Remove(SelectedTile);
        }

    }
}
