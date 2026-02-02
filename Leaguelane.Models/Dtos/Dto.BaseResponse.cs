namespace Leaguelane.Models.Dtos
{
    public class BaseResponseDto
    {
    }

    public record BaseResponse
    (
        bool IsSuccess,
        string Message,
        object? Data
    );
}
