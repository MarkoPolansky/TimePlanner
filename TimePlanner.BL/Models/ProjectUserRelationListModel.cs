namespace TimePlanner.BL.Models
{
    public record ProjectUserRelationListModel : ModelBase
    {
        public required Guid ProjectId { get; set; }
        public required Guid UserId { get; set; }
        public string? UserName { get; set; }
    }
}
