using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>, IUserModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper = new ActivityModelMapper();
    private readonly IProjectUserRelationModelMapper _projectUserRelationModelMapper;

    public UserModelMapper(IProjectUserRelationModelMapper projectUserRelationModelMapper)
    {
        _projectUserRelationModelMapper = projectUserRelationModelMapper;
    }

    public override UserListModel MapToListModel(UserEntity entity)
        => new UserListModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            ImageUrl = entity.ImageUrl
        };

    public UserListModel MapDetailToListModel(UserDetailModel model)
        => new UserListModel
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            ImageUrl = model.ImageUrl
        };

    public override UserDetailModel MapToDetailModel(UserEntity? entity)
    {
        if (entity is null)
        {
            return UserDetailModel.Empty;
        }

        return new UserDetailModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            ImageUrl = entity.ImageUrl,
            Projects = _projectUserRelationModelMapper.MapToListModel(entity.Projects).ToObservableCollection(),
            Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
        };
    }

    public override UserEntity MapToEntity(UserDetailModel model)
        => new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            ImageUrl = model.ImageUrl
        };
}