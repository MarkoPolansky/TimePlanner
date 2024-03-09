using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Mappers
{
    public class ActivityTypeEntityMapper : IEntityMapper<ActivityTypeEntity>
    {
        public void MapToExistingEntity(ActivityTypeEntity existingEntity, ActivityTypeEntity newEntity)
        {
            existingEntity.Name = newEntity.Name;
            existingEntity.User = newEntity.User;
        }
    }
}