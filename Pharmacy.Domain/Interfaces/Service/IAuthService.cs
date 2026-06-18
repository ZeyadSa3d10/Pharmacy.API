using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Common;
using Pharmacy.Domain.ApplicationDtos.AuthDtos;

namespace Pharmacy.Domain.Interfaces.Service
{
    public interface IAuthService
    {
        public Task<ApiResponse<AuthResultDto>> Login(LoginDto loginDto,CancellationToken cancellationToken);
        public Task<ApiResponse<AuthResultDto>> Register(RegisterDto registerDto,CancellationToken cancellationToken);
        public Task<ApiResponse<string>> SendOtp(string email,CancellationToken cancellationToken);
        public Task<ApiResponse<string>> ResendOtp(string email,CancellationToken cancellationToken);
        public Task<ApiResponse<string>> ResetPasswordAsync(ApplyNewPassword request,CancellationToken cancellationToken);
        public Task<ApiResponse<string>> IsvalidOtp(SendOtp sendOtp,CancellationToken cancellationToken);
    }
}
