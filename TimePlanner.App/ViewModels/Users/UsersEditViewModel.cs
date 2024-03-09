using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;

namespace TimePlanner.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UsersEditViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }
    public UserDetailModel User { get; set; } = UserDetailModel.Empty;
    public override IStateService StateService { get; }

    public UsersEditViewModel(
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

        var FetchedUser = await _userFacade.GetAsync(Id);

        if (FetchedUser != null)
        {
            this.User = FetchedUser;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _userFacade.SaveAsync(User);

        MessengerService.Send(new UserEditMessage());

        _navigationService.SendBackButtonPressed();
    }
}