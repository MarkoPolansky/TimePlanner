using CommunityToolkit.Mvvm.Input;
using TimePlanner.App.Services;
using TimePlanner.App.ViewModels;

namespace TimePlanner.App.Shells;

public partial class AppShell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();
    }
}
