using Minerva.Shared.Contract.Models;
using Minerva.Shared.Contract.Request.Account;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Mappers
{
    public interface IUserMapper : IMapper<UserEntity, UserModel>
    {
        UserEntity ToEntity(RegisterRequest request);
    }

    public class UserMapper : IUserMapper
    {
        public UserEntity ToEntity(UserModel model)
        {
            return new UserEntity()
            {
                Email = model.EmailAddress,
                UserName = model.Username,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName
            };
        }

        public UserModel ToModel(UserEntity entity)
        {
            return new UserModel()
            {
                EmailAddress = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
                Username = entity.UserName
            };
        }

        public UserEntity ToEntity(RegisterRequest request)
        {
            return new UserEntity()
            {
                MiddleName = request.MiddleName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.EmailAddress,
                PhoneNumber = request.PhoneNumber
            };
        }
    }
}