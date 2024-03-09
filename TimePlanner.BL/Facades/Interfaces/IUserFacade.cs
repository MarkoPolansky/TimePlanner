using TimePlanner.BL.Models;
using TimePlanner.DAL.Entities;

namespace TimePlanner.BL.Facades.Interfaces;

public interface IUserFacade : IFacade<UserEntity,UserListModel,UserDetailModel>
{
    Task<UserListModel?> GetListAsync(Guid id);
}