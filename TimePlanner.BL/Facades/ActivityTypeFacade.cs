using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;
using TimePlanner.DAL.Mappers;
using TimePlanner.DAL.UnitOfWork;

namespace TimePlanner.BL.Facades;

public class ActivityTypeFacade : FacadeBase<ActivityTypeEntity, ActivityTypeListModel, ActivityTypeDetailModel, ActivityTypeEntityMapper>,
    IActivityTypeFacade
{
    public ActivityTypeFacade(IUnitOfWorkFactory unitOfWorkFactory,
        IActivityTypeModelMapper modelMapper) 
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    protected override List<string> relationNames => new List<string> { nameof(ActivityTypeEntity.User) };
}