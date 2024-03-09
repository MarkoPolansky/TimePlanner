using TimePlanner.App.Models;
using TimePlanner.App.ViewModels;
using TimePlanner.App.Views.Activities;
using TimePlanner.App.ViewModels.ActivityTypes;
using TimePlanner.App.ViewModels.Settings;
using TimePlanner.App.Views.ActivityTypes;
using TimePlanner.App.Views.Settings;
using TimePlanner.App.Views.Users;
using TimePlanner.App.ViewModels.Project;
using TimePlanner.App.Views.Users;
using TimePlanner.App.Views.Project;

namespace TimePlanner.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//project", typeof(ProjectListView), typeof(ProjectListViewModel)),
        new("//project/projectDetail", typeof(ProjectDetailView), typeof(ProjectDetailViewModel)),

        new("//users", typeof(UsersListView), typeof(UsersListViewModel)),
        new("//users/edit", typeof(UsersEditView), typeof(UsersEditViewModel)),

        //new("//activities", typeof(ActivitiesListView), typeof(ActivitiesListViewModel)),
        new("//activities/edit", typeof(ActivitiesEditView), typeof(ActivitiesEditViewModel)),
        new("//activities/create", typeof(ActivitiesCreateView), typeof(ActivitiesCreateViewModel)),
        
        new("//settings", typeof(SettingsView), typeof(SettingsViewModel)),
        new("//activity-types/edit", typeof(ActivityTypeEditView), typeof(ActivityTypeEditViewModel)),
        

    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel 
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}