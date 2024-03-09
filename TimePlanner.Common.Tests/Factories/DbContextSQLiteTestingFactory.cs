using Microsoft.EntityFrameworkCore;
using TimePlanner.DAL;

namespace TimePlanner.Common.Tests.Factories;

public class DbContextSqLiteTestingFactory : IDbContextFactory<TimePlannerDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSqLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public TimePlannerDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TimePlannerDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        return new TimePlannerTestingDbContext(builder.Options, _seedTestingData);
    }
}