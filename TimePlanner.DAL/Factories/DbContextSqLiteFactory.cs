using Microsoft.EntityFrameworkCore;

namespace TimePlanner.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<TimePlannerDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<TimePlannerDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
    {
        this._seedTestingData = seedTestingData;

        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
    }

    public TimePlannerDbContext CreateDbContext() => new(this._contextOptionsBuilder.Options, this._seedTestingData);
}