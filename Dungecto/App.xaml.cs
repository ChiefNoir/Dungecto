using GalaSoft.MvvmLight.Threading;
using System.Windows;

namespace Dungecto
{
    /// <summary> Interaction logic for App.xaml </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
