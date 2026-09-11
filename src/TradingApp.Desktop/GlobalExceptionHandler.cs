using System;
using System.IO;
using System.Threading.Tasks;

namespace TradingApp.Desktop
{
    public static class GlobalExceptionHandler
    {
        public static void Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                LogException((Exception)args.ExceptionObject, "AppDomain.UnhandledException");
            };

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                LogException(args.Exception, "TaskScheduler.UnobservedTaskException");
                args.SetObserved();
            };
        }

        public static void LogException(Exception ex, string source)
        {
            try
            {
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var logPath = Path.Combine(desktopPath, "TradingApp_ErrorLog.txt");
                var message = $"[{DateTime.Now}] Source: {source}\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}\n\n";
                File.AppendAllText(logPath, message);
            }
            catch
            {
                // Ignored
            }
        }
    }
}
