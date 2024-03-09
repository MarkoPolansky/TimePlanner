using TimePlanner.App.Shells;

namespace TimePlanner.App;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<AppShell>();

    }
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        if (window != null)
        {
            window.Title = "TimePlanner";
            window.MaximumHeight = 600;
            window.MinimumHeight = 600;
            window.MaximumWidth = 1000;
            window.MinimumWidth = 1000;
        }

        
        return window;
    }
}