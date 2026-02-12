using Azure.Core;
using Leaguelane.Enums.Enums;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;

namespace Leaguelane.ApiService.Feature
{
    public class UserFeatureService: IUserFeatureService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly IPasswordResetTokenService _passwordResetTokenService;

        public UserFeatureService(IUserService userService, IConfiguration config, IEmailService emailService, IPasswordResetTokenService passwordResetTokenService)
        {
            _userService = userService;
            _config = config;
            _emailService = emailService;
            _passwordResetTokenService = passwordResetTokenService;
        }

        public async Task<BaseResponse> ForgetPassword(ForgotPasswordRequestDto forgotPasswordRequestDto, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByUserName(forgotPasswordRequestDto.Email, cancellationToken);

            if (user != null)
            {
                var tokenBytes = RandomNumberGenerator.GetBytes(64);
                var token = WebEncoders.Base64UrlEncode(tokenBytes);
                var tokenHash = SHA256.HashData(tokenBytes);

                //Store password reset token
                var resetToken = new PasswordResetToken
                {
                    UserId = user.UserId,
                    TokenHash = tokenHash,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                    Used = false
                };

                await _passwordResetTokenService.CreateAsync(resetToken, cancellationToken);

                var resetLink = $"{_config["FrontendUrl"]}/reset-password?token={token}";

                var resetPasswordParams = new
                {
                    Name = user.FirstName,
                    ResetLink = resetLink
                };

                await _emailService.SendEmailAsync(NotificationTypes.ForgotPassword, user.UserName, resetPasswordParams);

            }
            return new BaseResponse(true, "Password reset link sent to your email", null);
        }

        public async Task<BaseResponse> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto, CancellationToken cancellationToken)
        {
            if(resetPasswordRequestDto.Password != resetPasswordRequestDto.ConfirmPassword)
            {
                return new BaseResponse(false, "Password and confirm password do not match", null);
            }

            var urlDecodedToken = WebEncoders.Base64UrlDecode(resetPasswordRequestDto.Token);
            var tokenHash = SHA256.HashData(urlDecodedToken);

            //Get and validate password reset token

            var resetToken = await _passwordResetTokenService.GetTokenByTokenHashAsync(tokenHash, cancellationToken);

            if (resetToken == null || resetToken.ExpiresAt < DateTime.UtcNow || resetToken.Used)
            {
                return new BaseResponse(false, "Invalid token", null);
            }

            //Update password
            var user = await _userService.GetUserById(resetToken.UserId, cancellationToken);

            user.Password = resetPasswordRequestDto.Password;
            await _userService.UpdateUser(user, cancellationToken);

            //Mark token as used
            resetToken.Used = true;
            await _passwordResetTokenService.UpdateAsync(resetToken, cancellationToken);

            return new BaseResponse(true, "Password reset successfully", null);
        }

        public async Task<BaseResponse> ValidateResetPasswordToken(string token, CancellationToken cancellationToken)
        {
            var urlDecodedToken = WebEncoders.Base64UrlDecode(token);
            var tokenHash = SHA256.HashData(urlDecodedToken);

            var resetToken = await _passwordResetTokenService.GetTokenByTokenHashAsync(tokenHash, cancellationToken);

            if (resetToken == null || resetToken.ExpiresAt < DateTime.UtcNow || resetToken.Used)
            {
                throw new Exception( "Invalid token");
            }

            return new BaseResponse(true, "Validated token successfully", null);
        }
    }
}
