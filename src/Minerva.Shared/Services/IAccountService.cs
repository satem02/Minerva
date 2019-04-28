using System;
using System.Threading.Tasks;
using Minerva.Shared.Contract.Request.Account;
using Minerva.Shared.Contract.Response.Account;

namespace Minerva.Shared.Services
{
    public interface IAccountService : IDisposable
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LogoutResponse> LogoutAsync(LogoutRequest request);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request);
        Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request);
        Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request);
        Task<GetProfileResponse> GetProfileAsync(GetProfileRequest request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<GetUserResponse> GetUserAsync(GetUserRequest request);
    }
}
