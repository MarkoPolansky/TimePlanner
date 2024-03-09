using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;

namespace TimePlanner.App.ViewModels;

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}

public partial class ActivitiesListViewModel : ViewModelBase, IRecipient<ActivityEditMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;
    public string SelectedMode { get; set; } = "Day";
    public DateTime SelectedDateStart { get; set; } = DateTime.Now.Date;
    public DateTime SelectedDateEnd { get; set; } = DateTime.Now.Date;

    public override IStateService StateService { get; }

    public ActivitiesListViewModel(
        IActivityFacade activityFacade,
        IProjectFacade projectFacade,
        IStateService stateService,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _projectFacade = projectFacade;
        _navigationService = navigationService;

        StateService = stateService;

        // Set default to this day only
        SelectedDateEnd = SelectedDateEnd.AddDays(1);
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        var FetchedActivities = (await _activityFacade.GetAsync()).Where(activity => activity.UserId == StateService.CurrentUser.Id &&
        (activity.Start > SelectedDateStart && activity.End < SelectedDateEnd)).ToList();

        FetchedActivities.Sort((x, y) => x.Start.CompareTo(y.Start));

        Projects = await _projectFacade.GetMyAsync(this.StateService.CurrentUser.Id);
        Activities = FetchedActivities.Select((entity) =>
        {
            entity.ProjectName = Projects.SingleOrDefault(project => project.Id == entity.ProjectId).Name;

            return entity;
        }).ToObservableCollection();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync<ActivitiesCreateViewModel>();
    }

    [RelayCommand]
    private async Task GoToEditAsync(Guid Id)
    {
        await _navigationService.GoToAsync<ActivitiesEditViewModel>(new Dictionary<string, object?> { [nameof(UsersEditViewModel.Id)] = Id });
    }

    [RelayCommand]
    public async Task SubstractDateAsync()
    {
        if (SelectedMode == "Day")
        {
            SelectedDateStart = SelectedDateStart.AddDays(-1);
            SelectedDateEnd = SelectedDateEnd.AddDays(-1);
        }
        else if (SelectedMode == "Week")
        {
            SelectedDateStart = SelectedDateStart.StartOfWeek(DayOfWeek.Monday).AddDays(-7);
            SelectedDateEnd = SelectedDateStart;
            SelectedDateEnd = SelectedDateEnd.AddDays(7);
        }
        else if (SelectedMode == "Month")
        {
            SelectedDateStart = new DateTime(SelectedDateStart.Year, SelectedDateStart.Month, 1, 0, 0, 0).AddMonths(-1);
            SelectedDateEnd = SelectedDateStart;
            SelectedDateEnd = SelectedDateEnd.AddMonths(1);
        }

        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task IncrementDateAsync()
    {
        if (SelectedMode == "Day")
        {
            SelectedDateStart = SelectedDateStart.AddDays(1);
            SelectedDateEnd = SelectedDateEnd.AddDays(1);
        }
        else if (SelectedMode == "Week")
        {
            SelectedDateStart = SelectedDateStart.StartOfWeek(DayOfWeek.Monday).AddDays(7);
            SelectedDateEnd = SelectedDateStart;
            SelectedDateEnd = SelectedDateEnd.AddDays(7);
        }
        else if (SelectedMode == "Month")
        {
            SelectedDateStart = new DateTime(SelectedDateStart.Year, SelectedDateStart.Month, 1, 0, 0, 0).AddMonths(1);
            SelectedDateEnd = SelectedDateStart;
            SelectedDateEnd = SelectedDateEnd.AddMonths(1);
        }

        await LoadDataAsync();
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }
}
