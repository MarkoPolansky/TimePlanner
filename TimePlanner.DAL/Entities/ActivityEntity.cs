namespace TimePlanner.DAL.Entities
{
    public class ActivityEntity: IEntity
    {
        public Guid Id { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public required string Description { get; set; }

        public UserEntity? User { get; set; }
        public required Guid UserId { get; set; }
        public ProjectEntity? Project { get; set; }
        public required Guid ProjectId { get; set; }
        public ActivityTypeEntity? Type { get; set; }
        public Guid? TypeId { get; set; }
    }
}