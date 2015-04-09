using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dungecto.Common.Converters
{
    /// <summary> Converter from <seealso cref="bool"/> to <seealso cref="Visibility"/> with inversing </summary>
    public class InversedBoolToVisibilityConverter : IValueConverter
    {
        /// <summary>Convert bool to <seealso cref="Visibility"/> with inverse</summary>
        /// <param name="value"> <seealso cref="bool"/> value</param>
        /// <returns>
        /// <code>Visibility.Collapsed</code> if value is true 
        /// <code>Visibility.Visible</code> if value is false
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>NotImplementedException</summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
