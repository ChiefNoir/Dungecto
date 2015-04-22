using System;
using System.Globalization;
using System.Windows.Data;

namespace Dungecto.Common.Converters
{
    /// <summary> Inverse bool value </summary>
    public class InversedBoolToBoolConverter : IValueConverter
    {
        /// <summary> Inverse bool values </summary>
        /// <param name="value">bool value</param>
        /// <returns>Inversed value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        /// <summary> Inverse bool values </summary>
        /// <param name="value">bool value</param>
        /// <returns>Inversed value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
