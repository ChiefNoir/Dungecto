using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Dungecto.Common.Converters
{
    /// <summary><c>enum</c> to <c>Visibility</c> converter</summary>
    public class EnumsToVisibilityConverter : IValueConverter
    {
        /// <summary>Convert from <c>enum</c> to <c>Visibility</c> </summary>
        /// <param name="value">Enum value</param>
        /// <param name="parameter">enums values in format (string): "enumZero|enumOne|enumN", when converter returns <c>Visibility.Visible</c></param>
        /// <returns> <c>Visibility.Visible</c> if <see cref="value"/> is enum and in <see cref="parameter"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = parameter as string;
            if (param == null) { return DependencyProperty.UnsetValue; }

            if (Enum.IsDefined(value.GetType(), value) == false) { return DependencyProperty.UnsetValue; }

            if(param.Split('|').Any(x=> Enum.Parse(value.GetType(), x).Equals(value)))
            {
                return Visibility.Visible; 
            }

            return Visibility.Collapsed;
        }

        /// <summary> Not implemented </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
