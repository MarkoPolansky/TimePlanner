using Microsoft.EntityFrameworkCore;
using TimePlanner.Common.Tests;
using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Tests.Tests;

public class ActivityTypeTests: TestsBase
{
    [Fact]
    public async Task NewActivityType_Add_Added()
    {

        //Assert
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

        //Act
        _dbContextSUT.ActivityTypes.Add(activityType);
        await _dbContextSUT.SaveChangesAsync();


        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.ActivityTypes.Include(i=>i.User).SingleAsync(i => i.Id == activityType.Id);
        DeepAssert.Equal(activityType, actualEntities);
    }


}