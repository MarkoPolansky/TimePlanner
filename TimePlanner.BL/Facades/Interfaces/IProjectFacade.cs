using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Facades.Interfaces;

public interface IProjectFacade : IFacade<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
    Task<IEnumerable<ProjectListModel>> GetMyAsync(Guid UserId);
    Task<IEnumerable<ProjectListModel>> GetByNameAsync(string Name);
}