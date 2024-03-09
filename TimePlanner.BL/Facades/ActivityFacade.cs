using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;
using TimePlanner.DAL.Mappers;
using TimePlanner.DAL.Repositories;
using TimePlanner.DAL.UnitOfWork;

namespace TimePlanner.BL.Facades;

public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>,
    IActivityFacade
{
    public ActivityFacade(IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper) 
        : base(unitOfWorkFactory, modelMapper)
    {
    }
    protected override List<string> relationNames => new List<string> { nameof(ActivityEntity.Project), nameof(ActivityEntity.User), nameof(ActivityEntity.Type) };
    
    protected override bool Validate(IRepository<ActivityEntity> repository, ActivityDetailModel model)
    {
        // If activity duration is negative
        if (model.End < model.Start)
        {
            return false;
        }

        // Make sure activities do not exceed over each other
        if (repository.Get().Any(entity => (
            model.UserId == entity.UserId && model.Id != entity.Id &&
            ((model.End > entity.Start && entity.End == null) ||
            (model.Start < entity.End && model.Start > entity.Start))
        )))
        {
            return false;
        }

        // Make sure only one activity can be opened at one time
        if (model.End == null && repository.Get().Any(entity => entity.UserId == model.UserId && entity.End == null && entity.Id != model.Id))
        {
            return false;
        }

        return true;
    }
}