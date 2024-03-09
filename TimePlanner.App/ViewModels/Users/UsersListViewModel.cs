using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;

namespace TimePlanner.App.ViewModels;

public partial class UsersListViewModel : ViewModelBase, IRecipient<UserEditMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;
    public override IStateService StateService { get; }

    public UsersListViewModel(
        IUserFacade userFacade,
        IStateService stateService,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
        StateService = stateService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await _userFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync<UsersEditViewModel>();
    }

    [RelayCommand]
    private async Task GoToEditAsync(Guid Id)
    {
        await _navigationService.GoToAsync<UsersEditViewModel>(new Dictionary<string, object?> { [nameof(UsersEditViewModel.Id)] = Id });
    }

    [RelayCommand]
    private async Task SelectUserAsync(Guid Id)
    {
        var SelectedUser = Users.First(u => u.Id == Id);

        this.StateService.CurrentUser = SelectedUser;

        MessengerService.Send(new SelectedUserChangeMessage());

        await _navigationService.GoToAsync("//HomePage");
    }

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }
}
