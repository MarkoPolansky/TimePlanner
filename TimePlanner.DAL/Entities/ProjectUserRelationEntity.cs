namespace TimePlanner.DAL.Entities
{
    public class ProjectUserRelationEntity: IEntity
    {
        public Guid Id { get; set; }
        public ProjectEntity? Project { get; set; }
        public required Guid ProjectId { get; set; }
        public UserEntity? User { get; set; }
        public required Guid UserId { get; set; }
    }
}