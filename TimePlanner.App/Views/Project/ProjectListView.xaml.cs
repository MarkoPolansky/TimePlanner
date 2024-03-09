using TimePlanner.App.ViewModels.Project;

namespace TimePlanner.App.Views.Project;

public partial class ProjectListView
{
    private new readonly ProjectListViewModel ViewModel;

    public ProjectListView(ProjectListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();

        this.ViewModel = viewModel;
    }

    void OnFindMyProjectsChanged(object sender, CheckedChangedEventArgs e)
    {
        if (this.ViewModel.FindMyProjects)
        {
            this.ViewModel.GetMyProjects();
        }
        else
        {
            this.ViewModel.GetAllProjects();
        }
    }

    private void OnFindProjectNameChanged(object sender, TextChangedEventArgs e)
    {
        this.ViewModel.FilterProjects();
    }
}