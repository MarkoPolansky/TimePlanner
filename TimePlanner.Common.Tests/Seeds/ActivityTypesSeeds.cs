using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimePlanner.DAL.Entities;

namespace TimePlanner.Common.Tests.Seeds;

public static class ActivityTypesSeeds
{
    public static readonly ActivityTypeEntity ActivityType1 = new()
    {
        Id = Guid.Parse(input: "55ea1bc8-ed9a-4d6e-a7a2-440943f118da"),
        Name = "Type1",
        UserId = UserSeeds.User1.Id,
        User = null,
    };

    public static readonly ActivityTypeEntity ActivityType2 = new()
    {
        Id = Guid.Parse(input: "de8968ad-d51f-4752-a0a8-af91c6e599db"),
        Name = "Type2",
        UserId = UserSeeds.User1.Id,
        User = null,
    };

    public static readonly ActivityTypeEntity ActivityType3 = new()
    {
        Id = Guid.Parse(input: "be305f2a-baff-4a8d-8a68-0cc68b0eeeff"),
        Name = "Type3",
        UserId = UserSeeds.User1.Id,
        User = null,
    };

    public static readonly ActivityTypeEntity ActivityType4 = new()
    {
        Id = Guid.Parse(input: "ad55618a-89ce-4ec6-82e2-f55b390d8982"),
        Name = "Type1",
        UserId = UserSeeds.User2.Id,
        User = null,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ActivityTypeEntity>().HasData(
            ActivityType1,
            ActivityType2,
            ActivityType3,
            ActivityType4
        );

    }
}

