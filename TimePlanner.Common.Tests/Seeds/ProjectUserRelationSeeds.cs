using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using TimePlanner.DAL.Entities;

namespace TimePlanner.Common.Tests.Seeds;

public static class ProjectUserRelationSeeds
{
    public static readonly ProjectUserRelationEntity ProjectUser1 = new()
    {
        Id = Guid.Parse(input: "25b3902d-7d4f-4213-9cf0-112348f56238"),
        Project = null,
        ProjectId = Guid.Parse(input: "25b3902d-7d4f-4213-9cf0-112348f56238"),
        User = null,
        UserId = Guid.Parse(input: "23b3902d-7d4f-4213-9cf0-112348f56238"),
    };

    public static readonly ProjectUserRelationEntity ProjectUser2 = new()
    {
        Id = Guid.Parse(input: "26b3902d-7d4f-4213-9cf0-112348f56238"),
        Project = null,
        ProjectId = Guid.Parse(input: "25b3902d-7d4f-4213-9cf0-112348f56238"),
        User = null,
        UserId = Guid.Parse(input: "24b3902d-7d4f-4213-9cf0-112348f56238"),
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectUserRelationEntity>().HasData(
            ProjectUser1,
            ProjectUser2);
    }
}