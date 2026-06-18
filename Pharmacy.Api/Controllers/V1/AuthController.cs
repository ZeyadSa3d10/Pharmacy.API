using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Application.Services;
using Pharmacy.Domain.Exceptions;
using Pharmacy.Domain.ApplicationDtos.AuthDtos;

namespace Pharmacy.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await authService.Login(loginDto,cancellationToken);
                return StatusCode(result.StatusCode, result);
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found.");
            }
            catch (InvalidPasswordException)
            {
                return Unauthorized("Invalid password.");
            }
            catch (UserNotVertified)
            {
                return Unauthorized("User email is not verified.");
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto,CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await authService.Register(registerDto,cancellationToken);
                return StatusCode(result.StatusCode, result);
            }
            catch (UserFailerFromRegister)
            {
                return StatusCode(500, "Registration failed due to an internal error.");
            }
            catch (UserExcistException)
            {
                return Conflict("A user with this email already exists.");
            }
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await authService.SendOtp(request.Email,cancellationToken);
                return StatusCode(result.StatusCode, result);
            }
            catch (UserNotFoundException)
            {
                return NotFound("User with the provided email does not exist.");
            }
        }
        [HttpPost("ResendOtp")]
        public async Task<IActionResult> ResendOtp([FromBody] string email,CancellationToken cancellationToken)
        {
            var result = await authService.ResendOtp(email, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("IsValidOtp")]
        public async Task<IActionResult> IsValidOtp([FromBody] SendOtp request,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await authService.IsvalidOtp(request,cancellationToken);
                return StatusCode(result.StatusCode, result);
            }
            catch (OtpNotFoundException)
            {
                return NotFound("Otp not found for the provided email.");
            }
            catch (OtpUsedException)
            {
                return BadRequest("This Otp has already been used.");
            }
            catch (OtpExpireTime)
            {
                return BadRequest("This Otp has expired.");
            }
            catch (OtpInvaildException)
            {
                return BadRequest("The provided Otp is invalid.");

            }
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody]ApplyNewPassword applyNewPassword,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await authService.ResetPasswordAsync(applyNewPassword,cancellationToken);
                return StatusCode(result.StatusCode, result);
            }
            catch (UserSaveInDatabase)
            {
                return StatusCode(500, "Failed to save the new password. Please try again.");
            }



        }
    }
}
