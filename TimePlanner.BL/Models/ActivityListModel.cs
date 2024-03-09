namespace TimePlanner.BL.Models
{
    public record ActivityListModel : ModelBase
    {
        public required DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public required string Description { get; set; }

        public required Guid UserId { get; set; }
        public required Guid ProjectId { get; set; }
        public required string ProjectName { get; set; }

        public Guid? ActivityTypeId { get; set; }

        public static ActivityListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Start = DateTime.MinValue,
            End = null,
            Description = string.Empty,
            ProjectName = string.Empty,
            UserId = Guid.Empty,
            ProjectId = Guid.Empty,
            ActivityTypeId = null,
        };
    }
}
