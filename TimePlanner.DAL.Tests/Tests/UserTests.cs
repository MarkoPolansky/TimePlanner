using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TimePlanner.Common.Tests;
using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Tests.Tests;

public class UserTests : TestsBase
{
    [Fact]
    public async Task NewUser_Add_Added()
    {
        //Arrange 
        UserEntity user = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            FirstName = "Michal",
            LastName = "Slovak",
            ImageUrl = "asd",
        };

        //Act
        _dbContextSUT.Users.Add(user);
        await _dbContextSUT.SaveChangesAsync();
        

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Users.SingleAsync(i => i.Id == user.Id);
        DeepAssert.Equal(actualEntities, user);
    }

    [Fact]
    public async Task NewUserActivity_Add_Added()
    {
        UserEntity user = new()
        {
            FirstName = "Michal",
            LastName = "Slovak",
            ImageUrl = ""
        };

        ProjectEntity project = new()
        {
            Name = "Projekt"
        };

        ActivityEntity activity = new()
        {
            Id = Guid.Parse("28e0510b-f7b4-46d9-b12c-b486f5f6c3e2"),
            Start = new DateTime(2023,
                5,
                1,
                8,
                30,
                52),
            End = new DateTime(2023,
                5,
                1,
                10,
                30,
                52),
            Description = "Test description",
            User = user,
            Project = project,
            UserId = user.Id,
            ProjectId = project.Id,
            TypeId = null,
        };
        
        user.Activities.Add(activity);

        //Act
        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.Activities.Add(activity);
        await _dbContextSUT.SaveChangesAsync();


        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Users
            .Include(i=>i.Activities)
                .ThenInclude(e=>e.Project)
            .SingleAsync(i => i.Id == user.Id);
        DeepAssert.Equal(actualEntities, user);
    }
}