using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Mappers
{
    public interface IEntityMapper<in TEntity>
        where TEntity : IEntity
    {
        void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
    }
}
