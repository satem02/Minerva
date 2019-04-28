using System.Threading.Tasks;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Providers
{
    public interface IUserProvider
    {
        Task<string> LoginAsync(string username, string password);
        string GenerateToken(UserEntity userEntity);
        Task<bool> CreateAsync(UserEntity userEntity, string password);
        Task<string> GetForgotPasswordTokenAsync();
        string GetUserId();
    }
}