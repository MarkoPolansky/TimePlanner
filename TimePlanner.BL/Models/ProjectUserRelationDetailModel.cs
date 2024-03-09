namespace TimePlanner.BL.Models
{
    public record ProjectUserRelationDetailModel : ModelBase
    {
        public ProjectListModel? Project { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? UserId { get; set; }
        public UserListModel? User { get; set; }

        public static ProjectUserRelationDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Project = null,
            User = null
        };
    }
}
