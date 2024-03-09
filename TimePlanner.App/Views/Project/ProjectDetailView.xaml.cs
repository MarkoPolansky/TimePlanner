using TimePlanner.App.ViewModels;

namespace TimePlanner.App.Views.Project;

public partial class ProjectDetailView
{
    public ProjectDetailView(ProjectDetailViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
    }
}