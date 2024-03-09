using Microsoft.EntityFrameworkCore;
using TimePlanner.Common.Tests;
using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Tests.Tests;

public class ActivityTests: TestsBase
{
    [Fact]
    public async Task NewActivityWithType_Add_Added()
    {
        UserEntity user = new()
        {
            FirstName = "Michal",
            LastName = "Slovak",
            ImageUrl = ""
        };

        ActivityTypeEntity activityType = new()
        {
            Name = "Type of activity 1",
            UserId = user.Id,
            User = user
        };

        ProjectEntity project = new()
        {
            Name = "Project 1"
        };


        ActivityEntity activity = new()
        {
            User = user,
            Description = "Activity 1",
            Project = project,
            Type = activityType,
            UserId = user.Id,
            ProjectId = project.Id,
            TypeId = null
        };

        //Act
        _dbContextSUT.Users.Add(user);
        _dbContextSUT.ActivityTypes.Add(activityType);
        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.Activities.Add(activity);
        await _dbContextSUT.SaveChangesAsync();


        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Activities
            .Include(i => i.User)
            .Include(i => i.Project)
            .Include(i => i.Type)
            .SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(activity, actualEntities);

    }

    [Fact]
    public async Task NewActivity_Add_Added()
    {
        UserEntity user = new()
        {
            FirstName = "Michal",
            LastName = "Slovak",
            ImageUrl = ""
        };

        ProjectEntity project = new()
        {
            Name = "Project 1"
        };

        ActivityEntity activity = new()
        {
            User = user,
            Description = "Activity 1",
            Project = project,
            UserId = user.Id,
            ProjectId = project.Id,
            TypeId = null,
        };

        //Act
        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.Activities.Add(activity);
        await _dbContextSUT.SaveChangesAsync();


        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Activities
            .Include(i => i.User)
            .Include(i => i.Project)
            .SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(activity, actualEntities);

    }
}