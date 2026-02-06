using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IUserFeatureService
    {
        Task<BaseResponse> ForgetPassword(ForgotPasswordRequestDto forgotPasswordRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> ValidateResetPasswordToken(string token, CancellationToken cancellationToken);
    }
}
