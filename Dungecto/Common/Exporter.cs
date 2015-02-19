using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dungecto.Common
{
    /// <summary> <see cref="Canvas"/> exporter </summary>
    public static class Exporter
    {
        /// <summary>DPI</summary>
        private const double Dpi = 96d;

        /// <summary>Export canvas content to png file</summary>
        /// <param name="canvas">Canvas to export</param>
        /// <param name="filepath">File path</param>
        public static void ToPng(this Canvas canvas, string filepath)
        {
            var bounds = VisualTreeHelper.GetDescendantBounds(canvas);

            var rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, Dpi, Dpi, PixelFormats.Default);

            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                var vb = new VisualBrush(canvas);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }

            rtb.Render(dv);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                ms.Close();

                File.WriteAllBytes(filepath, ms.ToArray());
            }
        }
    }
}
