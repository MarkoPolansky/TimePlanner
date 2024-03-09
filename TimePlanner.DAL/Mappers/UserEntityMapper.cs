using TimePlanner.DAL.Entities;

namespace TimePlanner.DAL.Mappers
{
    public class UserEntityMapper : IEntityMapper<UserEntity>
    {
        public void MapToExistingEntity(UserEntity existingEntity, UserEntity newEntity)
        {
            existingEntity.FirstName = newEntity.FirstName;
            existingEntity.LastName = newEntity.LastName;
            existingEntity.ImageUrl = newEntity.ImageUrl;
        }
    }
}