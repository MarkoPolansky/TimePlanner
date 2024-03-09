using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TimePlanner.App.Messages;
using TimePlanner.App.Resources.Texts;
using TimePlanner.App.Services;
using TimePlanner.App.ViewModels;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.Common.Tests.Seeds;

namespace TimePlanner.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectDetailViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;
    private IMessengerService _messengerService;

    public override IStateService StateService { get; }
    public Guid Id { get; set; }
    public ProjectDetailModel Project { get; private set; } = ProjectDetailModel.Empty;
    public ObservableCollection<ActivityListModel> MyActivities { get; set; } = new();

    [ObservableProperty]
    public string newName;

    public ProjectDetailViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IStateService stateService,
        IAlertService alertService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _alertService = alertService;
        _messengerService = messengerService;
        StateService = stateService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await _projectFacade.GetAsync(Id);

        foreach (ActivityListModel activity in Project.Activities)
        {
            if (activity.UserId == this.StateService.CurrentUser.Id)
            {
                MyActivities.Add(activity);
            }
        }

        NewName = Project.Name;
    }

    [RelayCommand]
    private async Task ChangeNameAsync()
    {
        if (NewName == null || NewName == Project.Name || NewName == "")
        {
            return;
        }

        var project = new ProjectDetailModel()
        {
            Id = Id,
            Name = NewName,
        };

        await _projectFacade.SaveAsync(project);

        _messengerService.Send(new ProjectEditMessage());
    }

    [RelayCommand]
    private void CloseDetail()
    {
        _navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task DeleteProjectAsync()
    {
        bool confirmed = await Application.Current.MainPage.DisplayAlert("Delete Project", "Are you sure you want to delete the project?", "Yes", "No");

        if (confirmed)
        {
            await _projectFacade.DeleteAsync(Project.Id);

            _messengerService.Send(new ProjectDeleteMessage());

            _navigationService.SendBackButtonPressed();
        }
    }
}
