using TimePlanner.DAL.Entities;
using TimePlanner.DAL.Mappers;
using TimePlanner.DAL.Repositories;

namespace TimePlanner.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new();

    Task CommitAsync();
}