using Microsoft.EntityFrameworkCore;

namespace TimePlanner.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<TimePlannerDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<TimePlannerDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
