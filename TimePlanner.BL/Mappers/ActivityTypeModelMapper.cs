using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Mappers;

public class ActivityTypeModelMapper : ModelMapperBase<ActivityTypeEntity, ActivityTypeListModel, ActivityTypeDetailModel>,
    IActivityTypeModelMapper
{
    public override ActivityTypeListModel MapToListModel(ActivityTypeEntity entity)
        => new ActivityTypeListModel
        {
            Id = entity.Id,
            Name = entity.Name,
            UserId = entity.UserId,
        };

    public override ActivityTypeDetailModel MapToDetailModel(ActivityTypeEntity? entity)
    {
        if (entity is null)
        {
            return ActivityTypeDetailModel.Empty;
        }
    
        return new ActivityTypeDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            UserId = entity.UserId,
        };
    }

    public override ActivityTypeEntity MapToEntity(ActivityTypeDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            UserId = model.UserId,
        };
}