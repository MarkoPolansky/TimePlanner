using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Mappers;

public class ProjectUserRelationModelMapper : ModelMapperBase<ProjectUserRelationEntity,ProjectUserRelationListModel, ProjectUserRelationDetailModel>,
    IProjectUserRelationModelMapper
{
    private readonly IUserModelMapper _userModelMapper;
    private readonly IProjectModelMapper _projectModelMapper;

    public ProjectUserRelationModelMapper()
    {
        _userModelMapper = new UserModelMapper(this);
        _projectModelMapper = new ProjectModelMapper(this);
    }

    public override ProjectUserRelationListModel MapToListModel(ProjectUserRelationEntity entity)
    {
        string Name = "";
        if (entity != null && entity.User != null)
        {
            Name = entity.User.FirstName + " " + entity.User.LastName;
        }

        return new ProjectUserRelationListModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ProjectId = entity.ProjectId,
            UserName = Name
        };
    }

    public override ProjectUserRelationDetailModel MapToDetailModel(ProjectUserRelationEntity? entity)
    {
        if (entity is null)
        {
            return ProjectUserRelationDetailModel.Empty;
        }

        return new ProjectUserRelationDetailModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ProjectId = entity.ProjectId,
            //Project = _projectModelMapper.MapToListModel(entity.Project!),
            //User = _userModelMapper.MapToListModel(entity.User!)
        };
    }

    public override ProjectUserRelationEntity MapToEntity(ProjectUserRelationDetailModel model)
        => new()
        {
            Id = model.Id,
            UserId = model.User!.Id,
            ProjectId = model.Project!.Id,
            //Project = null,
            //User = null
        };
}