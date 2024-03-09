using TimePlanner.BL.Facades;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.Common.Tests;
using TimePlanner.Common.Tests.Seeds;
using Xunit.Abstractions;

namespace TimePlanner.BL.Tests;

public class ActivityTypeFacadeTests : FacadeTestsBase
{
    private readonly IActivityTypeFacade _activityTypeFacadSUT;

    public ActivityTypeFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityTypeFacadSUT = new ActivityTypeFacade(UnitOfWorkFactory, ActivityTypeModelMapper);
    }

    [Fact]
    public async Task NewActivityType_Insert_ActivityTypeAdded()
    {
        var user = new UserDetailModel()
        {
            FirstName = UserSeeds.User1.FirstName,
            LastName = UserSeeds.User1.LastName,
            ImageUrl = UserSeeds.User1.ImageUrl
        };

        var activityType = new ActivityTypeDetailModel
        {
            Id = Guid.Empty,
            Name = "School",
            UserId = UserSeeds.User1.Id,
        };

        activityType = await _activityTypeFacadSUT.SaveAsync(activityType);

        var savedActivityType = ActivityTypeModelMapper.MapToDetailModel((await DbContextFactory.CreateDbContextAsync()).ActivityTypes.Single(el => el.Id == activityType.Id));

        Assert.Equal(activityType, savedActivityType);
    }

    [Fact]
    public async Task GetActivityTypeOfUser_Select_ReturnsAll()
    {
        var types = await _activityTypeFacadSUT.GetAsync();
        types = types.Where(u => u.UserId == UserSeeds.User1.Id);

        DeepAssert.Equal(3, types.Count());
    }
}