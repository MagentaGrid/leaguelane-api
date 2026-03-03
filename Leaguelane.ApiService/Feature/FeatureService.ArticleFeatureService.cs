using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class ArticleFeatureService: IArticleFeatureService
    {
        private readonly IArticleService _articleService;

        public ArticleFeatureService(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<BaseResponse> GetAllArticles(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var data = await _articleService.GetArticlesAsync(page, pageSize, cancellationToken);

            return new BaseResponse(true, "Articles fetched successfully", data.Select(ArticleMapper.MapToDto).ToList());
        }

        public async Task<BaseResponse> GetArticleById(int articleId, CancellationToken cancellationToken)
        {
            var data = await _articleService.GetArticleByIdAsync(articleId, cancellationToken);

            return new BaseResponse(true, "Article fetched successfully", ArticleMapper.MapToDto(data));
        }

        public async Task<BaseResponse> CreateArticle(ArticleRequestDto articleRequestDto, CancellationToken cancellationToken)
        {
            await _articleService.CreateArticleAsync(ArticleMapper.MapToEntity(articleRequestDto), cancellationToken);

            return new BaseResponse(true, "Article added successfully", true);
        }

        public async Task<BaseResponse> UpdateArticle(ArticleUpdateRequestDto articleRequestDto, CancellationToken cancellationToken)
        {
            await _articleService.UpdateArticleAsync(articleRequestDto, cancellationToken);

            return new BaseResponse(true, "Article updated successfully", true);
        }

        public async Task<BaseResponse> DeleteArticle(int articleId, CancellationToken cancellationToken)
        {
            await _articleService.DeleteArticleAsync(articleId, cancellationToken);

            return new BaseResponse(true, "Article deleted successfully", true);
        }

        public async Task<BaseResponse> PublishArticle(int articleId, CancellationToken cancellationToken)
        {
            var data = await _articleService.PublishArticle(articleId, cancellationToken);

            return new BaseResponse(true, "Articles published successfully", true);
        }
    }
}
