using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;

namespace Leaguelane.Service.Services
{
    public class ArticleService: IArticleService
    {
        private readonly IRepository _repository;

        public ArticleService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Article>> GetArticlesAsync(int page, int pageSize, string status, CancellationToken cancellationToken)
        {
            var data = await _repository.FindAllAsync<Article>(x => (bool)x.Active, cancellationToken);

            if (status.ToLower() == "published")
            {
                data = data.Where(x => x.IsPublished == true);
            }
            else if (status.ToLower() == "unpublished")
            {
                data = data.Where(x => x.IsPublished == false);
            }

            return data.OrderByDescending(x => x.Created).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<Article> GetArticleByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _repository.FirstOrDefaultAsync<Article>(x => x.ArticleId == id, cancellationToken);
        }

        public async Task<bool> CreateArticleAsync(Article article, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(article, cancellationToken);
            await _repository.SaveChangesAsync<Article>(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateArticleAsync(ArticleUpdateRequestDto article, CancellationToken cancellationToken)
        {
            var articleToUpdate = await _repository.GetByIdAsync<Article>(article.ArticleId, cancellationToken);

            if (articleToUpdate == null) throw new Exception("Article not found");

            articleToUpdate.PublishDate = article.PublishDate;
            articleToUpdate.FullAnalysis = article.FullAnalysis;
            articleToUpdate.Headline = article.Headline;
            articleToUpdate.ShortIntro = article.ShortIntro;
            articleToUpdate.IsPublished = article.IsPublished;

            _repository.Update(articleToUpdate);
            await _repository.SaveChangesAsync<Article>(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteArticleAsync(int id, CancellationToken cancellationToken)
        {
            var article = await _repository.GetByIdAsync<Article>(id, cancellationToken);
            if (article == null) throw new Exception("Article not found");

            article.Active = false;
            await _repository.UpdateAsync(article);
            await _repository.SaveChangesAsync<Article>(cancellationToken);
            return true;
        }

        public async Task<bool> PublishArticle(int articleId, CancellationToken cancellationToken)
        {
            var articleToUpdate = await _repository.GetByIdAsync<Article>(articleId, cancellationToken);

            if (articleToUpdate == null) throw new Exception("Article not found");

            articleToUpdate.PublishDate = DateTime.UtcNow;
            articleToUpdate.IsPublished = true;

            _repository.Update(articleToUpdate);
            await _repository.SaveChangesAsync<Article>(cancellationToken);
            return true;
        }
    }
}
