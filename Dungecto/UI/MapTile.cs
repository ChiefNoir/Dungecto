using System.Windows;
using System.Windows.Controls;

namespace Dungecto.UI
{
    /// <summary> <seealso cref="Dungecto.UI.MapCanvas"/> tile </summary>
    public class MapTile : ContentControl
    {
        /// <summary> <see cref="IsSelected"/> DependencyProperty </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register
            (
                "IsSelected",
                typeof(bool),
                typeof(MapTile),
                new FrameworkPropertyMetadata(false)
            );

        /// <summary>Is tile selected?</summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}
