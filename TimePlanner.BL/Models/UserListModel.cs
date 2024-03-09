namespace TimePlanner.BL.Models
{
    public record UserListModel : ModelBase
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string ImageUrl { get; set; }

        public static UserListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            FirstName = string.Empty,
            LastName = string.Empty,
            ImageUrl = string.Empty
        };
    }
}
