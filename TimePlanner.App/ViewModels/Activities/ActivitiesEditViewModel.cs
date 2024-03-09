using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;

namespace TimePlanner.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivitiesEditViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }
    public TimeSpan ActivityStartTime { get; set; } = DateTime.Now.TimeOfDay;
    public TimeSpan ActivityEndTime { get; set; } = DateTime.Now.TimeOfDay;
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;
    public override IStateService StateService { get; }

    public ActivitiesEditViewModel(
        IActivityFacade activityFacade,
        IStateService stateService,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;

        StateService = stateService;
    }
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        var fetchedActivity = await _activityFacade.GetAsync(Id);

        if (fetchedActivity != null)
        {
            ActivityStartTime = fetchedActivity.Start.TimeOfDay;

            if (fetchedActivity.End != null)
            {
                ActivityEndTime = ((DateTime)fetchedActivity.End).TimeOfDay;
            }

            this.Activity = fetchedActivity;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            Activity.Start += ActivityStartTime;
            Activity.End += ActivityEndTime;

            await _activityFacade.SaveAsync(Activity);

            MessengerService.Send(new ActivityEditMessage());

            _navigationService.SendBackButtonPressed();
        }
        catch (ArgumentOutOfRangeException e)
        {
            await Application.Current.MainPage.DisplayAlert("Edit Activity", "An error occured. You cannot overlap activites, activity duration cannot be negative and you cannot have more than one open activity!", "Ok");
        }
    }
}