using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Mappers
{
    public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
    {
        public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity) 
        {
            existingEntity.Start = newEntity.Start;
            existingEntity.End = newEntity.End;
            existingEntity.Description = newEntity.Description;
            existingEntity.UserId = newEntity.UserId;
            existingEntity.User = newEntity.User;
            existingEntity.Project = newEntity.Project;
            existingEntity.ProjectId = newEntity.ProjectId;
            existingEntity.Type = newEntity.Type;
            existingEntity.TypeId = newEntity.TypeId;
        }
    }
}
