using TimePlanner.App.ViewModels;
using TimePlanner.App.ViewModels.Project;

namespace TimePlanner.App.Views.Activities;

public partial class ActivitiesListView
{
    private new readonly ActivitiesListViewModel ViewModel;

    public ActivitiesListView(ActivitiesListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();

        this.ViewModel = viewModel;
    }

    private async void SelectedModeChanged(object sender, EventArgs e)
    {
        if (ViewModel != null)
        {
            await ViewModel.SubstractDateAsync();
            await ViewModel.IncrementDateAsync();
        }
    }
}