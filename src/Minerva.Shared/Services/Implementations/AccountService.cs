using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Minerva.Shared.Contract.Request.Account;
using Minerva.Shared.Contract.Response.Account;
using Minerva.Shared.Mappers;
using Minerva.Shared.Providers;

namespace Minerva.Shared.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserMapper _userMapper;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUserProvider userProvider, IUserMapper userMapper, ILogger<AccountService> logger)
        {
            _userProvider = userProvider;
            _userMapper = userMapper;
            _logger = logger;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var response = new RegisterResponse();

            var userEntity = _userMapper.ToEntity(request);
            var isSuccess = await _userProvider.CreateAsync(userEntity, request.Password);

            response.StatusCode = (int) (isSuccess
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest);

            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = new LoginResponse();
            response.AccessToken = await _userProvider.LoginAsync(request.Username, request.Password);

            response.StatusCode = (int) (!string.IsNullOrEmpty(response.AccessToken)
                ? HttpStatusCode.OK
                : HttpStatusCode.Unauthorized);

            return response;
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var response = new ForgotPasswordResponse();
            await Task.Run(() => { });

            return response;
        }

        public Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<LogoutResponse> LogoutAsync(LogoutRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetProfileResponse> GetProfileAsync(GetProfileRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}