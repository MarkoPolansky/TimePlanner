using TimePlanner.BL.Facades;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.Common.Tests.Seeds;
using TimePlanner.Common.Tests;
using Xunit.Abstractions;

namespace TimePlanner.BL.Tests;

public class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;
    private readonly IProjectUserRelationFacade _projectUserRelationFacade;
    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
        _projectUserRelationFacade = new ProjectUserRelationFacade(UnitOfWorkFactory, ProjectUserRelationModelMapper);
    }

    [Fact]
    public async Task NewUser_Insert_ProjectAdded()
    {
        var user = new UserDetailModel()
        {
            Id = Guid.NewGuid(),
            FirstName = "name",
            LastName = "surname",
            ImageUrl = "aaa"
        };

        user = await _userFacadeSUT.SaveAsync(user);

        var savedUser = UserModelMapper.MapToDetailModel((await DbContextFactory.CreateDbContextAsync()).Users.Single(el => el.Id == user.Id));

        DeepAssert.Equal(user, savedUser);
    }

    [Fact]
    public async Task GetAllUsers_Select_ReturnsAll()
    {
        var users = await _userFacadeSUT.GetAsync();

        DeepAssert.Equal(2, users.Count());
    }

    [Fact]
    public async Task GetProjectsOfUser_Select_ReturnsAll()
    {
        var user = await _userFacadeSUT.GetAsync(UserSeeds.User1.Id);

        Assert.NotNull(user);
        Assert.NotEmpty(user.Projects);
    }

    [Fact]
    public async Task GetActivitiesOfUser_Select_ReturnsAll()
    {
        var user = await _userFacadeSUT.GetAsync(UserSeeds.User1.Id);

        Assert.NotNull(user);
        Assert.NotEmpty(user.Activities);
    }
}

