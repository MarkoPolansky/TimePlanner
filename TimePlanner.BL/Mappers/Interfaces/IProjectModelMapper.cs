using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Mappers.Interfaces;

public interface IProjectModelMapper : IModelMapper<ProjectEntity, ProjectListModel, ProjectDetailModel>
{

}