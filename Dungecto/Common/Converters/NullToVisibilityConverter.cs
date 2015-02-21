using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dungecto.Common.Converters
{
    /// <summary> <code>null</code> to  <code>Visibility</code> converter </summary>
    /// <remarks> If value is not null returns Visibile </remarks>
    public class NullToVisibilityConverter : IValueConverter
    {
        /// <summary> Convert from object to Visibility </summary>
        /// <param name="value">Object</param>
        /// <param name="targetType">~</param>
        /// <param name="parameter">None</param>
        /// <param name="culture">~</param>
        /// <returns> <code>Visibility.Visible</code> if value is not null</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary> Not Implemented</summary>        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
