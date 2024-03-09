using TimePlanner.BL.Models;

namespace TimePlanner.App.Services;

public interface IStateService
{
    public UserListModel CurrentUser { get; set; }
}
