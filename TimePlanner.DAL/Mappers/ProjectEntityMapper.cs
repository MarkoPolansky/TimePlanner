using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Mappers
{
    public class ProjectEntityMapper : IEntityMapper<ProjectEntity>
    {
        public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity)
        {
            existingEntity.Name = newEntity.Name;
        }
    }
}