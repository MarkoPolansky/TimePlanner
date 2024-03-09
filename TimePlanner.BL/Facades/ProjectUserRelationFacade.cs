using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Mappers;
using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;
using TimePlanner.DAL.Mappers;
using TimePlanner.DAL.Repositories;
using TimePlanner.DAL.UnitOfWork;

namespace TimePlanner.BL.Facades;

public class ProjectUserRelationFacade : FacadeBase<ProjectUserRelationEntity, ProjectUserRelationListModel,ProjectUserRelationDetailModel,ProjectUserRelationEntityMapper>,
    IProjectUserRelationFacade
{
    private readonly IProjectUserRelationModelMapper _projectUserRelationModelMapper = new ProjectUserRelationModelMapper();

    public ProjectUserRelationFacade(IUnitOfWorkFactory unitOfWorkFactory,
        IProjectUserRelationModelMapper modelMapper) 
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    protected override List<string> relationNames => new List<string> { nameof(ProjectUserRelationEntity.Project), nameof(ProjectUserRelationEntity.User) };
}