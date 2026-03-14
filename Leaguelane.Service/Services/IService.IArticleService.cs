using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Service.Services
{
    public interface IArticleService
    {
        Task<List<Article>> GetArticlesAsync(int page, int pageSize, string status, CancellationToken cancellationToken);
        Task<Article> GetArticleByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateArticleAsync(Article article, CancellationToken cancellationToken);
        Task<bool> UpdateArticleAsync(ArticleUpdateRequestDto article, CancellationToken cancellationToken);
        Task<bool> DeleteArticleAsync(int id, CancellationToken cancellationToken);
        Task<bool> PublishArticle(int articleId, CancellationToken cancellationToken);
    }
}
