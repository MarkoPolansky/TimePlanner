namespace TimePlanner.DAL.Entities
{
    public class UserEntity: IEntity
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string ImageUrl { get; set; }
        public ICollection<ProjectUserRelationEntity> Projects { get; init; } = new List<ProjectUserRelationEntity>();
        public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();
        public ICollection<ActivityTypeEntity> ActivityTypes { get; init; } = new List<ActivityTypeEntity>();
    }
}