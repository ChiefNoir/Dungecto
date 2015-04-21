using Dungecto.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace Dungecto.Converters
{
    class EditorModeToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EditorMode) { return Cursors.Arrow; }

            switch ((EditorMode)value)
            {
                case EditorMode.Tiler: return Cursors.Arrow;
                case EditorMode.Filler: return Cursors.Pen;
                case EditorMode.Eraser: return Cursors.Cross;
                case EditorMode.ColorPicker: return Cursors.Pen;
            }

            return Cursors.Arrow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
