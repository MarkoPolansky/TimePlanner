using Microsoft.EntityFrameworkCore;
using TimePlanner.BL.Mappers;
using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.Common.Tests;
using TimePlanner.Common.Tests.Factories;
using TimePlanner.DAL;
using TimePlanner.DAL.Mappers;
using TimePlanner.DAL.UnitOfWork;
using Xunit.Abstractions;

namespace TimePlanner.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!,seedTestingData: true);

        UserEntityMapper = new UserEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        ActivityEntityMapper = new ActivityEntityMapper();
        ActivityTypeEntityMapper = new ActivityTypeEntityMapper();
        ProjectUserRelationEntityMapper = new ProjectUserRelationEntityMapper();


        ProjectUserRelationModelMapper = new ProjectUserRelationModelMapper();
        UserModelMapper = new UserModelMapper(ProjectUserRelationModelMapper);
        ProjectModelMapper = new ProjectModelMapper(ProjectUserRelationModelMapper);
        ActivityModelMapper = new ActivityModelMapper();
        ActivityTypeModelMapper = new ActivityTypeModelMapper();
      


        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<TimePlannerDbContext> DbContextFactory { get; }

    protected UserEntityMapper UserEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected ActivityTypeEntityMapper ActivityTypeEntityMapper { get; }
    protected ProjectUserRelationEntityMapper ProjectUserRelationEntityMapper { get; }


    protected IUserModelMapper UserModelMapper { get; }
    protected IProjectModelMapper ProjectModelMapper { get; }
    protected IActivityModelMapper ActivityModelMapper { get; }
    protected IActivityTypeModelMapper ActivityTypeModelMapper { get; }
    protected IProjectUserRelationModelMapper ProjectUserRelationModelMapper { get; }

 
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
