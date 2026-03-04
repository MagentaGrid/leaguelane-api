using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IArticleFeatureService
    {
        Task<BaseResponse> GetAllArticles(int page, int pageSize, CancellationToken cancellationToken);
        Task<BaseResponse> GetArticleById(int articleId, CancellationToken cancellationToken);
        Task<BaseResponse> CreateArticle(ArticleRequestDto articleRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> UpdateArticle(ArticleUpdateRequestDto articleRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> DeleteArticle(int articleId, CancellationToken cancellationToken);
        Task<BaseResponse> PublishArticle(int userId, CancellationToken cancellationToken);
    }
}
