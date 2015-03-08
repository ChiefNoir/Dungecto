using System;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dungecto.Common.Utils
{
    ///// <summary>Parsers</summary>
    //static class Converters
    //{
    //    /// <summary> Convert path markup to <see cref="Geometry"/></summary>
    //    /// <param name="pathMarkup">Path markup data</param>
    //    /// <returns><see cref="Geometry"/></returns>
    //    public static Geometry FromPathMarkup(string pathMarkup)
    //    {
    //        var xaml = String.Format
    //            (
    //                CultureInfo.InvariantCulture,
    //                "<Path xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'><Path.Data>{0}</Path.Data></Path>",
    //                pathMarkup
    //            );

    //        var path = XamlReader.Parse(xaml) as Path;
    //        if (path == null) { return null; }

    //        Geometry geometry = path.Data;
    //        path.Data = null;

    //        return geometry;
    //    }
    //}
}
