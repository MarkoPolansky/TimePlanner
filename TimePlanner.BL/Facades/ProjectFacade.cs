using Microsoft.EntityFrameworkCore;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;
using TimePlanner.DAL.Mappers;
using TimePlanner.DAL.UnitOfWork;

namespace TimePlanner.BL.Facades;

public class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>,
    IProjectFacade
{
    public ProjectFacade(IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper modelMapper) 
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    public virtual async Task<IEnumerable<ProjectListModel>> GetMyAsync(Guid UserId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ProjectEntity> entities = uow
            .GetRepository<ProjectEntity, ProjectEntityMapper>()
            .Get();

        entities = entities.Where(p => p.Users.Any(u => u.UserId == UserId));
        List<ProjectEntity> filteredEntities = await entities.ToListAsync();

        return ModelMapper.MapToListModel(filteredEntities);
    }

    public virtual async Task<IEnumerable<ProjectListModel>> GetByNameAsync(string Name)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ProjectEntity> entities = uow
            .GetRepository<ProjectEntity, ProjectEntityMapper>()
            .Get();

        entities = entities.Where(p => p.Name.ToLower().Contains(Name.ToLower()));
        List<ProjectEntity> filteredEntities = await entities.ToListAsync();

        return ModelMapper.MapToListModel(filteredEntities);
    }

    protected override List<string> relationNames => new List<string> { nameof(ProjectEntity.Activities), nameof(ProjectEntity.Users), $"{nameof(ProjectEntity.Users)}.{nameof(ProjectUserRelationEntity.User)}" };
}