using Microsoft.Win32;

namespace Dungecto.Common
{
    /// <summary> Static dialogs </summary>
    public static class Dialogs
    {
        /// <summary> Show save xml dialog</summary>
        /// <param name="title">Window title</param>
        /// <returns>path to save point or null</returns>
        public static string ShowSaveXml(string title)
        {
            var dialog = new SaveFileDialog();

            dialog.Title = title;
            dialog.DefaultExt = ".xml";
            dialog.Filter = "XML files (*.xml)|*.xml";
            dialog.AddExtension = true;

            if(dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }

        /// <summary> Show save xml dialog</summary>
        /// <param name="title">Window title</param>
        /// <returns>path to save point or null</returns>
        public static string ShowOpenXml(string title)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = title;
            dialog.DefaultExt = ".xml";
            dialog.Filter = "XML files (*.xml)|*.xml";
            dialog.AddExtension = true;

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }


        internal static string ShowSavePng(string title)
        {
            var dialog = new SaveFileDialog();

            dialog.Title = title;
            dialog.DefaultExt = ".png";
            dialog.Filter = "png files (*.png)|*.png";
            dialog.AddExtension = true;

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
