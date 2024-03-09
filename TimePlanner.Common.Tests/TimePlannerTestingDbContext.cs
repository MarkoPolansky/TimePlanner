using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TimePlanner.Common.Tests.Seeds;
using TimePlanner.DAL;

namespace TimePlanner.Common.Tests;

public class TimePlannerTestingDbContext : TimePlannerDbContext
{
    private readonly bool _seedTestingData;
    public TimePlannerTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
        : base(contextOptions)
    {
        _seedTestingData = seedTestingData;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            UserSeeds.Seed(modelBuilder);
            ProjectSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);
            ActivityTypesSeeds.Seed(modelBuilder);
            ProjectUserRelationSeeds.Seed(modelBuilder);
        }
    }

}
