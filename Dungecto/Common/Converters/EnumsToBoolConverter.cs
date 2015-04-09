using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dungecto.Common.Converters
{
    class EnumsToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (parameterString == null) { return DependencyProperty.UnsetValue; }

            if (Enum.IsDefined(value.GetType(), value) == false) { return DependencyProperty.UnsetValue; }


            foreach (var item in parameterString.Split('|'))
            {
                var parameterValue = Enum.Parse(value.GetType(), item);

                if (parameterValue.Equals(value)) { return true; }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
