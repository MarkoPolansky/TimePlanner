using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;

namespace TimePlanner.App.ViewModels;

public partial class ActivitiesCreateViewModel : ViewModelBase
{
    private readonly IProjectUserRelationFacade _projectUserRelationFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly IActivityTypeFacade _activityTypeFacade;
    private readonly INavigationService _navigationService;

    public TimeSpan ActivityStartTime { get; set; } = DateTime.Now.TimeOfDay;
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;

    public ObservableCollection<ActivityTypeListModel> ActivityTypes { get; set; } = null!;
    public ProjectListModel? SelectedProject { get; set; } = null;
    public ActivityTypeListModel? SelectedActivityType{ get; set; } = null;
    public ObservableCollection<ProjectListModel> Projects { get; set; } = null!;
    public override IStateService StateService { get; }

    public ActivitiesCreateViewModel(
        IActivityFacade activityFacade,
        IProjectUserRelationFacade projectUserRelationFacade,
        IProjectFacade projectFacade,
        IActivityTypeFacade activityTypeFacade,
        IStateService stateService,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _projectFacade = projectFacade;                             
        _projectUserRelationFacade = projectUserRelationFacade;
        _navigationService = navigationService;
        _activityTypeFacade = activityTypeFacade;

        StateService = stateService;

        Activity.Start = DateTime.Now;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        var ProjectsAll = await _projectFacade.GetAsync();
        var ProjectUserRelations = (await _projectUserRelationFacade.GetAsync()).Where(projectUserRelation => projectUserRelation.UserId == StateService.CurrentUser.Id);
        
        ActivityTypes = new ObservableCollection<ActivityTypeListModel>(await _activityTypeFacade.GetAsync());
        Projects = new ObservableCollection<ProjectListModel>(ProjectsAll.Where(project => ProjectUserRelations.Any(projectUserRelation => projectUserRelation.ProjectId == project.Id)));
    }

    [RelayCommand]
    private async Task SaveActivityAsync()
    {
        if (SelectedProject == null)
        {
            await Application.Current.MainPage.DisplayAlert("Create Activity", "You need to choose a project.", "Ok");
            return;
        }

        Activity.Start += ActivityStartTime;
        Activity.UserId = this.StateService.CurrentUser.Id;
        Activity.ProjectId = SelectedProject.Id;

        if (SelectedActivityType != null)
        {
            Activity.ActivityTypeId = SelectedActivityType.Id;
        }

        try
        {
            await _activityFacade.SaveAsync(Activity);
            MessengerService.Send(new ActivityEditMessage());

            _navigationService.SendBackButtonPressed();
        }
        catch (ArgumentOutOfRangeException e)
        {
            await Application.Current.MainPage.DisplayAlert("Create Activity", "An error occured. You cannot overlap activites, activity duration cannot be negative and you cannot have more than one open activity!", "Ok");
        }
    }
}