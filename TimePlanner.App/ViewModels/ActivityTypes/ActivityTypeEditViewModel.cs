using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;
using Windows.Devices.Sensors;
using TimePlanner.Common.Tests.Seeds;
using TimePlanner.BL.Facades;

namespace TimePlanner.App.ViewModels.ActivityTypes;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityTypeEditViewModel: ViewModelBase
{
    private readonly IActivityTypeFacade _activityTypeFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }
    
    public ActivityTypeDetailModel ActivityType { get; set; } = ActivityTypeDetailModel.Empty;
    public override IStateService StateService { get; }

    public ActivityTypeEditViewModel(
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

        var fetchedActivityType = await _activityTypeFacade.GetAsync(Id);

        if (fetchedActivityType != null)
        {
            this.ActivityType = fetchedActivityType;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        ActivityType.UserId = StateService.CurrentUser.Id;

        await _activityTypeFacade.SaveAsync(ActivityType);

        MessengerService.Send(new ActivityTypeEditMessage());

        _navigationService.SendBackButtonPressed();
    }
}