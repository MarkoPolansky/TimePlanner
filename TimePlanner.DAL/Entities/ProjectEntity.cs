namespace TimePlanner.DAL.Entities
{
    public class ProjectEntity: IEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
        public ICollection<ProjectUserRelationEntity> Users { get; init; } = new List<ProjectUserRelationEntity>();
    }
}