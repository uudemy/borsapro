namespace TradingApp.Desktop;

public static class Program
{
    // MAUI Uygulamasının giriş noktası
#if WINDOWS
    [global::System.STAThreadAttribute]
    public static void Main(string[] args)
    {
        global::Microsoft.UI.Xaml.Application.Start((p) => {
            var context = new global::Microsoft.Maui.Hosting.MauiWinUIApplication();
            context.GetType().GetProperty("MauiProgram")?.SetValue(context, MauiProgram.CreateMauiApp());
        });
    }
#endif
}
