using System;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    }

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

        public Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            throw new System.NotImplementedException();
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

    public interface IUserMapper : IMapper<UserEntity, UserModel>
    {
        UserEntity ToEntity(RegisterRequest request);
    }

    public interface IMapper<TEntity, TModel>
    {
        TEntity ToEntity(TModel model);
        TModel ToModel(TEntity entity);
    }

    public class UserModel
    {
    }

    public class UserEntity : IdentityUser
    {
    }

    public class EntityBase
    {
    }

    public interface IUserProvider
    {
        Task<string> LoginAsync(string username, string password);
        string GenerateToken(UserEntity userEntity);
        Task<bool> CreateAsync(UserEntity userEntity, string password);
        Task<string> GetForgotPasswordTokenAsync();
    }

    public class UserProvider : IUserProvider
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProvider(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager,
            IOptions<JwtIssuerOptions> jwtOptions, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions?.Value;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                return GenerateToken(user);
            }

            return null;
        }

        public string GenerateToken(UserEntity userEntity)
        {
            /*var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bhl1lIyDBPPyeXj8TCLnHd1YI1NMTD6S"));
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userEntity.Email)
            };
            var token = new JwtSecurityToken(
                issuer: "MusicDb",
                audience: "MusicDb",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;*/
            return "";
        }

        public async Task<bool> CreateAsync(UserEntity userEntity, string password)
        {
            var result = await _userManager.CreateAsync(userEntity, password);

            return result.Succeeded;
        }

        public async Task<string> GetForgotPasswordTokenAsync()
        {
            var userClaims = _httpContextAccessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null || !user.EmailConfirmed)
            {
                throw new Exception("Unauthorized Process");
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
    }

    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);

        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());

        //public SigningCredentials SigningCredentials { get; set; }
    }
}