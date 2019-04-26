using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minerva.Shared.Contract.Models;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Providers
{
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
            if (!result.Succeeded)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(username);
            return GenerateToken(user);

        }

        public string GenerateToken(UserEntity userEntity)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bhl1lIyDBPPyeXj8TCLnHd1YI1NMTD6S"));
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
            return jwtToken;
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
}