using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Mappers.Interfaces;

public interface IProjectUserRelationModelMapper
    : IModelMapper<ProjectUserRelationEntity, ProjectUserRelationListModel, ProjectUserRelationDetailModel>
{
}