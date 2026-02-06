using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using System.Security.Cryptography;

namespace Leaguelane.ApiService.Feature
{
    public class UserFeatureService: IUserFeatureService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UserFeatureService(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        public async Task<BaseResponse> ForgetPassword(ForgotPasswordRequestDto forgotPasswordRequestDto, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByUserName(forgotPasswordRequestDto.Email, cancellationToken);

            if (user != null)
            {
                var tokenBytes = RandomNumberGenerator.GetBytes(64);
                var token = Convert.ToBase64String(tokenBytes);
                var tokenHash = SHA256.HashData(tokenBytes);

                var resetLink = $"{_config["FrontendUrl"]}/reset-password?token={token}";

            }
            return new BaseResponse(true, "Password reset link sent to your email", null);
        }

        public async Task<BaseResponse> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto, CancellationToken cancellationToken)
        {
            return new BaseResponse(true, "Password reset successfully", null);
        }

        public async Task<BaseResponse> ValidateResetPasswordToken(string token, CancellationToken cancellationToken)
        {
            return new BaseResponse(true, "Validated token successfully", null);
        }
    }
}
