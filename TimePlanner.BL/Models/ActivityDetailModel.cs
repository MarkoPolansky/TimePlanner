namespace TimePlanner.BL.Models
{
    public record ActivityDetailModel : ModelBase
    {
        public required DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public required string Description { get; set; }

        public required Guid UserId { get; set; }
        public UserDetailModel? User { get; set; }

        public required Guid ProjectId { get; set; }
        public ProjectDetailModel? Project { get; set; }

        public Guid? ActivityTypeId { get; set; }
        public ActivityDetailModel? ActivityType { get; set; }
        
        public static ActivityDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Start = DateTime.MinValue,
            End = null,
            Description = string.Empty,
            UserId = Guid.Empty,
            User = null,
            ProjectId = Guid.Empty,
            Project = null,
            ActivityTypeId = null,
            ActivityType = null
        };
    }
}
