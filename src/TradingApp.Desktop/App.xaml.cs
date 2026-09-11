namespace TradingApp.Desktop;

public partial class App : Application
{
    public App()
    {
        GlobalExceptionHandler.Initialize();
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(new MainPage()));
    }
}
