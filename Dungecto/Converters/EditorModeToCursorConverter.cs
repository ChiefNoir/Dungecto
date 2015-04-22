using Dungecto.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace Dungecto.Converters
{
    /// <summary> Convert <see cref="Dungecto.ViewModel.EditorMode"/> to <see cref="System.Windows.Input.Cursor"/> </summary>
    class EditorModeToCursorConverter : IValueConverter
    {
        /// <summary></summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((EditorMode)value)
            {
                case EditorMode.Tiler: return Cursors.Arrow;
                case EditorMode.Filler: return Cursors.Pen;
                case EditorMode.ColorPicker: return Cursors.Pen;
                case EditorMode.Eraser: return Cursors.Cross;                
            }

            return Cursors.Arrow;
        }

        /// <summary> Not implemented</summary>        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
