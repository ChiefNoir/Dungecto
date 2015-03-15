using System;
using System.Globalization;
using System.Windows.Data;

namespace Dungecto.Common.Converters
{
    /// <summary> Convert object null state to bool, inversed </summary>
    class InversedNullToBoolConverter : IValueConverter 
    {
        /// <summary> Convert object null state to bool, inversed</summary>
        /// <param name="value">Anything</param>
        /// <returns>object is null — false</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        /// <summary> [Not implemented] </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
