using TimePlanner.App.ViewModels;

namespace TimePlanner.App.Views.Activities;

public partial class ActivitiesCreateView
{
    public ActivitiesCreateView(ActivitiesCreateViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}