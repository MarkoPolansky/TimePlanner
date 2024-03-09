using Microsoft.EntityFrameworkCore;
using TimePlanner.DAL.Entities;

namespace TimePlanner.Common.Tests.Seeds;

public static class ActivitySeeds
{


    public static readonly ActivityEntity ActivityEnded = new()
    {
        Id = Guid.Parse(input: "30b3902d-7d4f-4213-9cf0-112348f56238"),
        //Start = DateTime.Now.AddDays(-1),
        Start = new DateTime(2023, 5, 4, 10, 10, 0),
        End = new DateTime(2023, 5, 5, 00, 00, 0),
        Description = "C# learning",
        UserId = UserSeeds.User2.Id,
        ProjectId = ProjectSeeds.Project1.Id,
        TypeId = null
    };

    public static readonly ActivityEntity ActivityNotEnded = new()
    {
        Id = Guid.Parse(input: "31b3902d-7d4f-4213-9cf0-112348f56238"),
        Start = new DateTime(2023, 5, 5, 10, 10, 0),
        Description = "C# learning 2",
        UserId = UserSeeds.User2.Id,
        ProjectId = ProjectSeeds.Project1.Id,
        TypeId = null
    };


    public static readonly ActivityEntity Activity1 = new()
    {
        Id = Guid.Parse(input: "32b3902d-7d4f-4213-9cf0-112348f56238"),
        Start = new DateTime(2023, 5, 6, 10, 10, 0),
        End = new DateTime(2023, 5, 7, 00, 00, 0),
        Description = "Java learning",
        UserId = UserSeeds.User1.Id,
        ProjectId = ProjectSeeds.Project1.Id,
        TypeId = null
    };

    public static readonly ActivityEntity Activity2 = new()
    {
        Id = Guid.Parse(input: "33b3902d-7d4f-4213-9cf0-112348f56238"),
        Start = new DateTime(2023, 5, 25, 10, 10, 0),
        End = new DateTime(2023, 5, 26, 12, 10, 0),
        Description = "Java learning",
        UserId = UserSeeds.User1.Id,
        ProjectId = ProjectSeeds.Project1.Id,
        TypeId = null
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEnded,
            ActivityNotEnded,
            Activity1,
            Activity2
            );
    }
}