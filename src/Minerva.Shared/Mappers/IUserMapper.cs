using Minerva.Shared.Contract.Models;
using Minerva.Shared.Contract.Request.Account;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Mappers
{
    public interface IUserMapper : IMapper<UserEntity, UserModel>
    {
        UserEntity ToEntity(RegisterRequest request);
    }
}