using Microsoft.EntityFrameworkCore;
using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL;

public class TimePlannerDbContext : DbContext
{
    private readonly bool seedDemoData;

    public TimePlannerDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : base(contextOptions)
    {
        this.seedDemoData = seedDemoData;
    }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<ProjectUserRelationEntity> ProjectUserRelations => Set<ProjectUserRelationEntity>();
    public DbSet<ActivityTypeEntity> ActivityTypes => Set<ActivityTypeEntity>();
}
