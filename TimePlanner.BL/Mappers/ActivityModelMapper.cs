using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Mappers;

public class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>,
    IActivityModelMapper
{
    public override ActivityListModel MapToListModel(ActivityEntity entity)
        => new ActivityListModel
        {
            Id = entity.Id,
            Start = entity.Start,
            End = entity.End,
            Description = entity.Description,
            ProjectName = "",
            UserId = entity.UserId,
            ProjectId = entity.ProjectId,
            ActivityTypeId = entity.TypeId,
        };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
    {
        if (entity is null)
        {
            return ActivityDetailModel.Empty;
        }

        return new ActivityDetailModel
        {
            Id = entity.Id,
            Start = entity.Start,
            End = entity.End,
            Description = entity.Description,
            UserId = entity.UserId,
            ProjectId = entity.ProjectId,
            ActivityTypeId = entity.TypeId,
        };
    }

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            Start = model.Start,
            End = model.End,
            Description = model.Description,
            ProjectId = model.ProjectId,
            UserId = model.UserId,
            TypeId = model.ActivityTypeId,
        };
}