using Microsoft.EntityFrameworkCore;
using TimePlanner.DAL.Entities;

namespace TimePlanner.Common.Tests.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity User1 = new()
    {
        Id = Guid.Parse(input: "23b3902d-7d4f-4213-9cf0-112348f56238"),
        ImageUrl = "Asd",
        FirstName = "Michal",
        LastName = "Slovak"
    };

    public static readonly UserEntity User2 = new()
    {
        Id = Guid.Parse(input: "24b3902d-7d4f-4213-9cf0-112348f56238"),
        ImageUrl = "sss",
        FirstName = "Michal",
        LastName = "Kovač"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            User1,
            User2);
    }

}