using TimePlanner.BL.Facades;
using TimePlanner.BL.Models;
using TimePlanner.Common.Tests.Seeds;
using TimePlanner.Common.Tests;
using Xunit.Abstractions;
using System.Collections.ObjectModel;

namespace TimePlanner.BL.Tests;

public class ProjectFacadeTests : FacadeTestsBase
{
    private readonly ProjectFacade _projectFacadeSUT;
    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task NewProjectNoUserNoActivity_Insert_ProjectAdded()
    {
        var project = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Project1"
        };

        project = await _projectFacadeSUT.SaveAsync(project);

        var savedProject = ProjectModelMapper.MapToDetailModel((await DbContextFactory.CreateDbContextAsync()).Projects.Single(el => el.Id == project.Id));

        DeepAssert.Equal(project, savedProject);
    }

    [Fact]
    public async Task NewProjectWithActivity_Insert_Throws()
    {
        var project = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Project1",
            Activities = new ObservableCollection<ActivityListModel>()
            {
                ActivityModelMapper.MapToListModel(ActivitySeeds.Activity1),
            },
        };

        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _projectFacadeSUT.SaveAsync(project));
    }

    [Fact]
    public async Task EditProjectName()
    {
        var project = new ProjectDetailModel()
        {
            Id = ProjectSeeds.Project1.Id,
            Name = "Project1_changed",
        };

        await _projectFacadeSUT.SaveAsync(project);

    }

    [Fact]
    public async Task GetAllProjects_Select_ReturnsAll()
    {
        var projects = await _projectFacadeSUT.GetAsync();

        DeepAssert.Equal(2, projects.Count());
    }

    [Fact]
    public async Task GetUsersByOnProject_Select_ReturnsOne()
    {
        var project = await _projectFacadeSUT.GetAsync(ProjectSeeds.Project1.Id);

        Assert.NotNull(project);
        Assert.NotEmpty(project.Users);
    }

    [Fact]
    public async Task GetAllActivitiesOnProject_Select_ReturnsAll()
    {
        var project = await _projectFacadeSUT.GetAsync(ProjectSeeds.Project1.Id);

        Assert.NotNull(project);
        Assert.NotEmpty(project.Activities);
    }
}