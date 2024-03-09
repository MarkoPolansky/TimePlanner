using Microsoft.EntityFrameworkCore;
using TimePlanner.Common.Tests.Factories;
using TimePlanner.DAL.Factories;
using Xunit.Abstractions;

namespace TimePlanner.DAL.Tests.Tests;

public class TestsBase : IAsyncLifetime
{
    protected TestsBase()
    {

        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!);

        _dbContextSUT = DbContextFactory.CreateDbContext();

    }

    protected IDbContextFactory<TimePlannerDbContext> DbContextFactory { get; }

    protected TimePlannerDbContext _dbContextSUT;
    
    public async Task InitializeAsync()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.DisposeAsync();
    }
}
