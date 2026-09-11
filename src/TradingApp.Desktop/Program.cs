#if WINDOWS
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.UI.Xaml;

namespace TradingApp.Desktop.WinUI
{
    public class WinUIApp : MauiWinUIApplication
    {
        public WinUIApp()
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
#endif

namespace TradingApp.Desktop
{
    public static class Program
    {
#if WINDOWS
        [global::System.STAThreadAttribute]
        public static void Main(string[] args)
        {
            global::Microsoft.UI.Xaml.Application.Start((p) => new WinUI.WinUIApp());
        }
#endif
    }
}
