using BloggingPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Domain.Repositories
{
    public interface IArticleRepository
    {
        Task<Article?> GetByIdAsync(Guid id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<IEnumerable<Article>> GetByAuthorIdAsync(Guid authorId);
        Task AddAsync(Article article);
        void Update(Article article);
        void Delete(Article article);
    }
}
