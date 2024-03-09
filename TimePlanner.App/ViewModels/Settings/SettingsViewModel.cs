using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;
using TimePlanner.App.ViewModels.ActivityTypes;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;

namespace TimePlanner.App.ViewModels.Settings;

public partial class SettingsViewModel: ViewModelBase, IRecipient<ActivityTypeEditMessage>
{
    private readonly IActivityTypeFacade _activityTypeFacade;
    private readonly INavigationService _navigationService;
    public IEnumerable<ActivityTypeListModel> ActivityTypes { get; set; } = null!;

    public override IStateService StateService { get; }

    public SettingsViewModel(
        IActivityTypeFacade activityFacade,
        IStateService stateService,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _activityTypeFacade = activityFacade;
        _navigationService = navigationService;
        StateService = stateService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        ActivityTypes = await _activityTypeFacade.GetAsync();
    }

    [RelayCommand]
    private async Task DeleteAsync(Guid Id)
    {
        bool confirmed = await Application.Current.MainPage.DisplayAlert("Delete Activity Type", "Are you sure you want to delete this activity type?", "Yes", "No");

        if (confirmed)
        {
            await _activityTypeFacade.DeleteAsync(Id);
            MessengerService.Send(new ActivityTypeEditMessage());
        }
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("//activity-types/edit");
    }

    [RelayCommand]
    private async Task GoToEditAsync(Guid Id)
    {
        await _navigationService.GoToAsync("//activity-types/edit", new Dictionary<string, object?> { [nameof(ActivityTypeEditViewModel.Id)] = Id });
    }

    [RelayCommand]
    private async Task GoToUsersAsync()
    {
        await _navigationService.GoToAsync("//users");
    }

    public async void Receive(ActivityTypeEditMessage message)
    {
        await LoadDataAsync();
    }
}