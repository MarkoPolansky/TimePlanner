using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using TimePlanner.Common.Tests;
using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Tests.Tests;

public class ProjectTests : TestsBase
{
    [Fact]
    public async Task NewProject_Add_Added()
    {
        ProjectEntity project = new()
        {
            Id = Guid.Parse("ac660796-cfd5-416f-9f24-c2d0082d4323"),
            Name = "Projekt 1" 
        };

        //Act
        _dbContextSUT.Projects.Add(project);
        await _dbContextSUT.SaveChangesAsync();


        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Projects.SingleAsync(i => i.Id == project.Id);
        DeepAssert.Equal(actualEntities, project);
    }

    [Fact]
    public async Task NewProjectUser_Add_Added()
    {
        UserEntity user = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            FirstName = "Michal",
            LastName = "Slovak",
            ImageUrl = "",
        };

        ProjectEntity project = new()
        {
            Id = Guid.Parse("ac660796-cfd5-416f-9f24-c2d0082d4323"),
            Name = "Projekt 1"
        };

        ProjectUserRelationEntity projectUser = new() 
        { 
            Id = Guid.Parse("efd9b957-675f-4622-b2de-215b86156239"),
            UserId = user.Id,
            ProjectId = project.Id
        };

        project.Users.Add(projectUser);

        //Act
        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.ProjectUserRelations.Add(projectUser);
        await _dbContextSUT.SaveChangesAsync();


        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.ProjectUserRelations.Include(i=>i.Project).Include(i=>i.User).SingleAsync(i => i.Id == projectUser.Id);
        DeepAssert.Equal(actualEntities, projectUser);
    }

    [Fact]
    public async Task NewProjectActivity_Add_Added()
    {
        ProjectEntity project = new()
        {
            Name = "Projekt 3"
        };

        UserEntity user = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            FirstName = "Michaela",
            LastName = "Novotna",
            ImageUrl = "",
        };

        ActivityTypeEntity type = new()
        {
            Name = "Tag 1",
            UserId = user.Id,
            User = null
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
            Type = type,
            UserId = user.Id,
            ProjectId = project.Id,
            TypeId = null
        };


        //Act
        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.ActivityTypes.Add(type);
        _dbContextSUT.Activities.Add(activity);
        await _dbContextSUT.SaveChangesAsync();


        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Activities.Include(i => i.Project).Include(i => i.User).Include(i=>i.Type).SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(actualEntities, activity);
    }
}