using Microsoft.EntityFrameworkCore.Design;

namespace TimePlanner.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TimePlannerDbContext>
{
    private readonly DbContextSqLiteFactory dbContextFactory;

    public DesignTimeDbContextFactory()
    {
        this.dbContextFactory = new DbContextSqLiteFactory("TimePlanner");
    }

    public TimePlannerDbContext CreateDbContext(string[] args) => this.dbContextFactory.CreateDbContext();
}