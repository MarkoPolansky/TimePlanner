using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Mappers
{
    public class ProjectUserRelationEntityMapper : IEntityMapper<ProjectUserRelationEntity>
    {
        public void MapToExistingEntity(ProjectUserRelationEntity existingEntity, ProjectUserRelationEntity newEntity)
        {
            existingEntity.Project = newEntity.Project;
            existingEntity.User = newEntity.User;
        }
    }
}