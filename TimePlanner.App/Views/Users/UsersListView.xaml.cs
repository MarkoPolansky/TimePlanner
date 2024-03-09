using TimePlanner.App.ViewModels;

namespace TimePlanner.App.Views.Users;

public partial class UsersListView
{
	public UsersListView(UsersListViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}
}