namespace TimePlanner.DAL.Entities
{
    public class ActivityTypeEntity : IEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public UserEntity? User { get; set; }
        public required Guid UserId { get; set; }
    }
}