using TimePlanner.BL.Facades;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.Common.Tests;
using TimePlanner.Common.Tests.Seeds;
using Xunit.Abstractions;

namespace TimePlanner.BL.Tests;

public class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacade;
    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacade = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task NewActivity_InsertOrUpdate_ActivityAdded()
    {
        var activity = new ActivityDetailModel
        {
            Id = Guid.Empty,
            Description = "Activity 1",
            Start = DateTime.Now,
            UserId = UserSeeds.User1.Id,
            ProjectId = ProjectSeeds.Project1.Id,
        };
    
        activity = await _activityFacade.SaveAsync(activity);

        var savedActivity = ActivityModelMapper.MapToDetailModel((await DbContextFactory.CreateDbContextAsync()).Activities.Single(el => el.Id == activity.Id)); 

        Assert.Equal(activity, savedActivity);
    }

    [Fact]
    public async Task NewActivityExceed_InsertOrUpdate_ThrowsError()
    {
        var activity = new ActivityDetailModel
        {
            Id = Guid.Empty,
            Description = "Activity Bad",
            Start = new DateTime(2023, 8, 5, 10, 10, 0),
            UserId = UserSeeds.User2.Id,
            ProjectId = ProjectSeeds.Project1.Id,
        };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _activityFacade.SaveAsync(activity));
    }

    [Fact]
    public async Task NewActivityMultipleOngoing_InsertOrUpdate_ThrowsError()
    {
        var activity = new ActivityDetailModel
        {
            Id = Guid.Empty,
            Description = "Activity Bad",
            Start = new DateTime(2023, 10, 5, 10, 10, 0),
            UserId = UserSeeds.User2.Id,
            ProjectId = ProjectSeeds.Project1.Id,
        };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _activityFacade.SaveAsync(activity));
    }

    [Fact]
    public async Task GetActivitiesOfUser_Select_ReturnsAll()
    {
        var activities = await _activityFacade.GetAsync();
        activities = activities.Where(u => u.UserId == UserSeeds.User1.Id);

        DeepAssert.Equal(2, activities.Count());
    }

    [Fact]
    public async Task DeleteActivity_Delete_GetsDeleted()
    {
        await _activityFacade.DeleteAsync(ActivitySeeds.Activity1.Id);

        var activity = await _activityFacade.GetAsync(ActivitySeeds.Activity1.Id);

        Assert.Null(activity);
    }

    [Fact]
    public async Task UpdateActivity_Update_GetsUpdated()
    {
        var activity = ActivityModelMapper.MapToDetailModel(ActivitySeeds.Activity1);
        activity.Description = "Changed activity name 1";

        var savedActivity = await _activityFacade.SaveAsync(activity);

        DeepAssert.Equal(activity, savedActivity);
    }

    [Fact]
    public async Task UpdateActivity2_Update_GetsUpdated()
    {
        var activity = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityNotEnded);
        activity.End = new DateTime(2023, 5, 5, 12, 10, 0);

        var savedActivity = await _activityFacade.SaveAsync(activity);

        DeepAssert.Equal(activity, savedActivity);
    }
}