using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minerva.API.Common;
using Minerva.Shared.Contract.Request.Account;
using Minerva.Shared.Contract.Response.Account;
using Minerva.Shared.Services;

namespace Minerva.API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _accountService.LoginAsync(request);
            return Result(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _accountService.RegisterAsync(request);
            return Result(response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            ForgotPasswordResponse response = await _accountService.ForgotPasswordAsync(request);
            return Result(response);
        }

        [HttpPatch("reset-password/{activationCode}")]
        public async Task<IActionResult> ResetPassword([FromRoute] Guid activationCode,
            [FromBody] ResetPasswordRequest request)
        {
            ResetPasswordResponse response = await _accountService.ResetPasswordAsync(request);
            return Result(response);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> Me([FromQuery]GetProfileRequest request)
        {
            GetProfileResponse response = await _accountService.GetProfileAsync(request);
            return Result(response);
        }

        [Authorize]
        [HttpPatch("")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountRequest request)
        {
            UpdateAccountResponse response = await _accountService.UpdateAccountAsync(request);
            return Result(response);
        }

        [Authorize]
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            ChangePasswordResponse response = await _accountService.ChangePasswordAsync(request);
            return Result(response);
        }

        [Authorize]
        [HttpDelete("")]
        public async Task<IActionResult> Delete([FromQuery] DeleteAccountRequest request)
        {
            DeleteAccountResponse response = await _accountService.DeleteAccountAsync(request);
            return Result(response);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout([FromQuery] LogoutRequest request)
        {
            LogoutResponse response = await _accountService.LogoutAsync(request);
            return Result(response);
        }

    }
}