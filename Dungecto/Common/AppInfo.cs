using System.Diagnostics;
using System.Reflection;

namespace Dungecto.Common
{
    /// <summary> Load info from <seealso cref="Assembly"/></summary>
    public static class AppInfo
    {
        /// <summary>Get product version from app Assembly</summary>
        /// <returns>Product version</returns>
        public static string GetProductVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return fileVersionInfo.ProductVersion;
        }

        /// <summary>Get legal copyright from app Assembly</summary>
        /// <returns>Legal copyright </returns>
        public static string GetLegalCopyright()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return fileVersionInfo.LegalCopyright;
        }
    }
}
