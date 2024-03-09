namespace TimePlanner.BL.Models
{
    public record ActivityTypeDetailModel : ModelBase
    {
        public required string Name { get; set; }

        public Guid UserId { get; set; }

        public static ActivityTypeDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            UserId = Guid.Empty
        };
    }
}
