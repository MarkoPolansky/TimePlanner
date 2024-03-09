using TimePlanner.App.ViewModels;

namespace TimePlanner.App.Views.Activities;

public partial class ActivitiesEditView
{
    public ActivitiesEditView(ActivitiesEditViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}