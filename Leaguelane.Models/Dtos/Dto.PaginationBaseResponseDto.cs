namespace Leaguelane.Models.Dtos
{
    public class PaginationBaseResponseDto
    {
    }

    public record PaginationBaseResponse
    (
        bool IsSuccess,
        string Message,
        object? Data,
        int Page = 0,
        int PageSize = 0,
        int TotalCount = 0,
        int TotalPages = 0
    );
}
