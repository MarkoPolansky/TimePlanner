using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TimePlanner.App.Messages;
using TimePlanner.App.Services;

namespace TimePlanner.App.ViewModels;

public abstract class ViewModelBase : ObservableRecipient, IViewModel, IRecipient<SelectedUserChangeMessage>
{
    private bool _isRefreshRequired = true;

    protected readonly IMessengerService MessengerService;

    public abstract IStateService StateService { get; }

    protected ViewModelBase(
        IMessengerService messengerService)
        : base(messengerService.Messenger)
    {
        MessengerService = messengerService;
        IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (_isRefreshRequired)
        {
            await LoadDataAsync();

            _isRefreshRequired = false;
        }
    }
    public async void Receive(SelectedUserChangeMessage message)
    {
        await LoadDataAsync();
    }

    protected virtual Task LoadDataAsync()
        => Task.CompletedTask;
}
