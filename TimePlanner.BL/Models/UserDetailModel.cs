using System.Collections.ObjectModel;

namespace TimePlanner.BL.Models
{
    public record UserDetailModel : ModelBase
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string ImageUrl { get; set; }
        
        public ObservableCollection<ProjectUserRelationListModel> Projects { get; init; } = new();
        public ObservableCollection<ActivityListModel> Activities { get; init; } = new();
        public ObservableCollection<ActivityTypeListModel> ActivityTypes { get; init; } = new();

        public static UserDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            FirstName = string.Empty, 
            LastName = string.Empty, 
            ImageUrl = string.Empty
        };
    }
}
