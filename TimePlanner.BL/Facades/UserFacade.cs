using Microsoft.EntityFrameworkCore;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;
using TimePlanner.DAL.Mappers;
using TimePlanner.DAL.UnitOfWork;

namespace TimePlanner.BL.Facades;

public class UserFacade : FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>, IUserFacade
{
    public UserFacade(IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    protected override UserDetailModel RemoveCollections(UserDetailModel model)
    {
        model.ActivityTypes.Clear();
        model.Projects.Clear();
        model.Activities.Clear();

        return model;
    }

    public virtual async Task<UserListModel?> GetListAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<UserEntity> query = uow.GetRepository<UserEntity, UserEntityMapper>().Get();

        // Include all provided relations, applies only for querying TDetailModel so should not cause an issue with fetching useless information.
        //foreach (string key in this.relationNames)
        //{
        //    query = query.Include(key);
        //}

        UserEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        return entity is null
            ? null
            : ModelMapper.MapToListModel(entity);
    }

    protected override List<string> relationNames => new List<string> { nameof(UserEntity.Activities), nameof(UserEntity.Projects), nameof(UserEntity.ActivityTypes) };
}