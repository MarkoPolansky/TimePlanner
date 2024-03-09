using System.Collections.ObjectModel;

namespace TimePlanner.BL.Models
{
    public record ProjectDetailModel : ModelBase
    {
        public required string Name { get; set; }

        public ObservableCollection<ActivityListModel> Activities { get; init; } = new();
        public ObservableCollection<ProjectUserRelationListModel> Users { get; init; } = new();

        public static ProjectDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty
        };
    }
}
