using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Runtime.CompilerServices;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;
using TimePlanner.BL.Facades;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Mappers;
using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.Common.Tests.Seeds;

namespace TimePlanner.App.ViewModels.Project;

public partial class ProjectListViewModel : ViewModelBase, IRecipient<ProjectDeleteMessage>, IRecipient<ProjectEditMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly IProjectUserRelationFacade _projectUserRelationFacade;
    private readonly IUserFacade _userFacade;
    private readonly IUserModelMapper _userModelMapper;
    private readonly INavigationService _navigationService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

    public override IStateService StateService { get; }

    [ObservableProperty]
    public bool findMyProjects;

    [ObservableProperty]
    public string newProjectName;

    [ObservableProperty]
    public string findProjectName;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        IProjectUserRelationFacade projectUserRelationFacade,
        IUserFacade userFacade,
        IUserModelMapper userModelMapper,
        IStateService stateService,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _projectUserRelationFacade = projectUserRelationFacade;
        _userFacade = userFacade;
        _userModelMapper = userModelMapper;
        StateService = stateService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetAsync();
    }

    public async void GetMyProjects()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetMyAsync(this.StateService.CurrentUser.Id);
    }

    public async void GetAllProjects()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetAsync();
    }

    public async void FilterProjects()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetByNameAsync(this.FindProjectName);
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<ProjectDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.Id)] = id });
    }

    [RelayCommand]
    private async Task AddNewProjectAsync(string ProjectName)
    {
        if (ProjectName == null || ProjectName == "")
        {
            return;
        }
        
        var project = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = ProjectName
        };

        await _projectFacade.SaveAsync(project);

        NewProjectName = "";

        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task JoinProjectAsync(Guid ProjectId)
    {
        var User = await _userFacade.GetAsync(this.StateService.CurrentUser.Id);

        var ProjectDetail = await _projectFacade.GetAsync(ProjectId);

        if (ProjectDetail.Users.Any(p => p.UserId == this.StateService.CurrentUser.Id))
        {
            await Application.Current.MainPage.DisplayAlert("Join Project", "You are already member of this project.", "Ok");
            return;
        }

        var ListUser = _userModelMapper.MapDetailToListModel(User);
        
        var Project = Projects.FirstOrDefault(obj => obj.Id == ProjectId);

        var UserProjectRelation = new ProjectUserRelationDetailModel()
        {
            Id = Guid.NewGuid(),
            Project = Project,
            ProjectId = ProjectId,
            User = ListUser,
            UserId = ListUser.Id
        };

        await _projectUserRelationFacade.SaveAsync(UserProjectRelation);

        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
    }
}
