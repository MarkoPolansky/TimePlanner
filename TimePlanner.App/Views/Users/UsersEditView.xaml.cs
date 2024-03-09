using TimePlanner.App.ViewModels;

namespace TimePlanner.App.Views.Users;

public partial class UsersEditView
{
    public UsersEditView(UsersEditViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}