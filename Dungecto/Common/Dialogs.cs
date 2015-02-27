using Microsoft.Win32;
using System;
using System.Globalization;

namespace Dungecto.Common
{
    /// <summary> Static dialogs </summary>
    public static class Dialogs
    {
        /// <summary>Show open file win32 dialog</summary>
        /// <param name="title">Title of the window</param>
        /// <param name="extension">File extension. Example: ".xml" </param>
        /// <returns>Filepath or <c>null</c> if nothing was selected</returns>
        public static string ShowOpenDialog(string title, string extension)
        {
            return ShowFileDialog(new OpenFileDialog(), title, extension);
        }

        /// <summary>Show save file win32 dialog</summary>
        /// <param name="title">Title of the window</param>
        /// <param name="extension">File extension. Example: ".xml" </param>
        /// <returns>Filepath or <c>null</c> if nothing was selected</returns>
        public static string ShowSaveDialog(string title, string extension)
        {
            return ShowFileDialog(new SaveFileDialog(), title, extension);
        }

        /// <summary> Show win32 file dialog window </summary>
        /// <param name="dialog">Dialog window</param>
        /// <param name="title">Title of the window</param>
        /// <param name="extension">File extension. Example: ".xml" </param>
        /// <returns>Filepath or <c>null</c> if nothing was selected</returns>
        private static string ShowFileDialog(FileDialog dialog, string title, string extension)
        {
            dialog.Title = title;
            dialog.DefaultExt = extension;
            dialog.Filter = String.Format(CultureInfo.InvariantCulture, "{0}-files (*{0})|*{0}", extension);
            dialog.AddExtension = true;

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}