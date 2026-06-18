using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Domain.Common;
using Pharmacy.Domain.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using Pharmacy.Domain.Models.Auth;
using Azure.Core;
using Microsoft.AspNetCore.Identity.Data;
using Pharmacy.Domain.Exceptions;
using System.Security.Cryptography;
using Pharmacy.Domain.ApplicationDtos.AuthDtos;
using System.Threading;

namespace Pharmacy.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        private readonly IUnitOfWork unitOfWork;

        public AuthService(ITokenService tokenService,IConfiguration configuration,IEmailService emailService,IUnitOfWork unitOfWork)
        {
            this._tokenService = tokenService;
            this.configuration = configuration;
            this.emailService = emailService;
            this.unitOfWork = unitOfWork;
        }


        public async Task<ApiResponse<AuthResultDto>> Login(LoginDto loginDto,CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserReposatory.GetUserAsync(loginDto.Email, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            //if (user.Vertified==false) throw new UserNotVertified();
            if (user == null)
                throw new UserNotFoundException();
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new InvalidPasswordException();
            var token = _tokenService.GenerateToken(user, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            AuthResultDto authResult = new AuthResultDto
            {
                Token = token,

            };
            return ApiResponse<AuthResultDto>.Success(authResult);
        }
        public async Task<ApiResponse<AuthResultDto>> Register(RegisterDto registerDto,CancellationToken cancellationToken)
        {
           
               var user = await unitOfWork.UserReposatory.GetUserAsync(registerDto.Email, cancellationToken);
                if (user != null)
                    throw new UserExcistException();
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, 10);
                //string id = Guid.NewGuid().ToString();
                var newUser = new User
                {
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    FullName = registerDto.FullName,
                    PasswordHash = passwordHash,
                    //Id = id,
                    Role = Domain.Enums.Roles.User
                };
                var result =await unitOfWork.UserReposatory.AddAsync(newUser, cancellationToken);
                var token =  _tokenService.GenerateToken(newUser, cancellationToken);
                var Saving = await unitOfWork.SaveChangesAsync(cancellationToken);
                if (Saving > 0)
                {
                   AuthResultDto authResult = new AuthResultDto
                   {
                       Token = token,
                       
                   };
                    return ApiResponse<AuthResultDto>.Success(authResult, 201);
                }

                 throw new UserFailerFromRegister();

        }

        public async Task<ApiResponse<string>> SendOtp(string email,CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserReposatory.GetUserAsync(email, cancellationToken);
            if (user == null)
            {

                await Task.Delay(1000, cancellationToken); // 1 seconds
                return ApiResponse<string>.Success("Reset Otp sent to your email.");

            }
            var Otp = new Random().Next(100000, 999999).ToString();
           
            var HashedOtp =BCrypt.Net.BCrypt.HashPassword(Otp);

           
            var record = new PasswordResetCode
            {
                Email = email,
                HashedOtp = HashedOtp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false,
                UserId=user.Id
            };

            await unitOfWork.PasswordResetCodeGenerateReposatory.AddAsync(record, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            string body = $@"
        <h3>Password Reset</h3>
        <p>Use the Otp below to reset your password:</p>
        <p><b>Email:</b> {email}</p>
        <P><b>Otp : </b> {Otp}</p>

        <p>Go to the reset password page and enter this Otp To Reset Password.</p>
    ";

            await emailService.SendAsync(email, "Reset Your Password", body);

            return ApiResponse<string>.Success("Reset Otp sent to your email.");
        }
        public async Task<ApiResponse<string>> ResendOtp(string email,CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserReposatory.GetUserAsync(email, cancellationToken);
            if (user == null)
            {

                await Task.Delay(1000, cancellationToken); // 1 seconds
                return ApiResponse<string>.Success("Reset Otp sent to your email.");

            }
            var Otp = new Random().Next(100000, 999999).ToString();
           
            var HashedOtp =BCrypt.Net.BCrypt.HashPassword(Otp);

           
            var record = new PasswordResetCode
            {
                Email = email,
                HashedOtp = HashedOtp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false,
                UserId=user.Id
            };

            var lastRecord = await unitOfWork.PasswordResetCodeGenerateReposatory.GetLastCodeByEmail(email, cancellationToken);
            lastRecord.IsUsed = true;

            await unitOfWork.PasswordResetCodeGenerateReposatory.AddAsync(record, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            string body = $@"
        <h3>Password Reset</h3>
        <p>Use the Otp below to reset your password:</p>
        <p><b>Email:</b> {email}</p>
        <P><b>Otp : </b> {Otp}</p>

        <p>Go to the reset password page and enter this Otp To Reset Password.</p>
    ";

            await emailService.SendAsync(email, "Reset Your Password", body);

            return ApiResponse<string>.Success("Reset Otp sent to your email.");
        }

        public async Task<ApiResponse<string>> IsvalidOtp(SendOtp resetPasswordRequest,CancellationToken cancellationToken)
        {
            var record = await unitOfWork.PasswordResetCodeGenerateReposatory
                .GetLastCodeByEmail(resetPasswordRequest.Email, cancellationToken);

            if (record == null)
                throw new OtpNotFoundException();

            if (record.IsUsed)
                throw new OtpUsedException();

            if (record.ExpirationTime < DateTime.UtcNow)
                throw new OtpExpireTime();

            bool isValid = BCrypt.Net.BCrypt.Verify(resetPasswordRequest.Otp, record.HashedOtp);

            if (!isValid)
                throw new OtpInvaildException();

            record.IsUsed = true;
            var result = await unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<string>.Success("Otp Is Correct");
        }
        public async Task<ApiResponse<string>> ResetPasswordAsync(ApplyNewPassword request,CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return ApiResponse<string>.ErrorResponse("Passwords do not match.");

            var user = await unitOfWork.UserReposatory.GetUserAsync(request.Email,cancellationToken);
            var UserToken = _tokenService.GenerateToken(user, cancellationToken);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.PasswordHash = hashedPassword;
            unitOfWork.UserReposatory.Update(user, cancellationToken);
            var result = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (result <= 0)
                throw new UserSaveInDatabase();
            return ApiResponse<string>.Success("Password reset successfully.");
        }

       
    }
}
