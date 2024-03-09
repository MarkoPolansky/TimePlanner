namespace TimePlanner.BL.Models
{
    public record ActivityTypeListModel : ModelBase
    {
        public required string Name { get; set; }

        public Guid UserId { get; set; }

        public static ActivityTypeListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            UserId = Guid.Empty,
        };
    }
}
