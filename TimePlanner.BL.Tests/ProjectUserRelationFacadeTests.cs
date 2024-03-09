using TimePlanner.BL.Facades;
using TimePlanner.BL.Models;
using TimePlanner.Common.Tests.Seeds;
using TimePlanner.Common.Tests;
using Xunit.Abstractions;
using System.Collections.ObjectModel;

namespace TimePlanner.BL.Tests;

public class ProjectUserRelationFacadeTests : FacadeTestsBase
{
    private readonly ProjectUserRelationFacade _projectUserRelationFacadeSUT;
    public ProjectUserRelationFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectUserRelationFacadeSUT = new ProjectUserRelationFacade(UnitOfWorkFactory, ProjectUserRelationModelMapper);
    }

    [Fact]
    public async Task NewProjectNoUserNoActivity_Insert_ProjectAdded()
    {
        var Project = ProjectModelMapper.MapToListModel(ProjectSeeds.Project2);
        var User = UserModelMapper.MapToListModel(UserSeeds.User2);
        var projectUser = new ProjectUserRelationDetailModel()
        {
            Id = Guid.NewGuid(),
            Project = Project,
            ProjectId = Project.Id,
            User = User,
            UserId = User.Id
        };

        await _projectUserRelationFacadeSUT.SaveAsync(projectUser);

    }

}