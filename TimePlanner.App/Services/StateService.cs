using CommunityToolkit.Mvvm.ComponentModel;
using TimePlanner.BL.Models;

namespace TimePlanner.App.Services;

public class StateService : ObservableObject, IStateService
{
    public UserListModel CurrentUser { get; set; } = UserListModel.Empty;
}