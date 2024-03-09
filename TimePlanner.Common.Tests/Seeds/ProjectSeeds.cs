using Microsoft.EntityFrameworkCore;
using TimePlanner.DAL.Entities;

namespace TimePlanner.Common.Tests.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity Project1 = new()
    {
        Id = Guid.Parse(input: "25b3902d-7d4f-4213-9cf0-112348f56238"),
        Name = "Project 1"
    };

    public static readonly ProjectEntity Project2 = new()
    {
        Id = Guid.Parse(input: "26b3902d-7d4f-4213-9cf0-112348f56238"),
        Name = "Project 2",
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            Project1,
            Project2);
    }
}