using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Mappers;

public class ProjectModelMapper : ModelMapperBase<ProjectEntity,ProjectListModel,ProjectDetailModel>,IProjectModelMapper
{
    private IActivityModelMapper _activityModelMapper = new ActivityModelMapper();
    private IProjectUserRelationModelMapper _projectUserRelationModelMapper;

    public ProjectModelMapper(IProjectUserRelationModelMapper projectUserRelationModelMapper)
    {
        _projectUserRelationModelMapper = projectUserRelationModelMapper;
    }

    public override ProjectListModel MapToListModel(ProjectEntity entity)
        => new ProjectListModel
        {
            Id = entity.Id,
            Name = entity.Name,
        };

    public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
    {
        if (entity is null)
        {
            return ProjectDetailModel.Empty;
        }

        return new ProjectDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Users = _projectUserRelationModelMapper.MapToListModel(entity.Users).ToObservableCollection(),
            Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
        };
    }

    public override ProjectEntity MapToEntity(ProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name
        };
}