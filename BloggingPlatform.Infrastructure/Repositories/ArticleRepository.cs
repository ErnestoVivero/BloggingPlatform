using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Domain.Repositories;
using BloggingPlatform.Infrastructure.Pertistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Article article) => await _context.AddAsync(article);

        public void Delete(Article article) => _context.Remove(article);

        public async Task<IEnumerable<Article>> GetAllAsync() => await _context.Articles.Include(a => a.Author).ToListAsync();

        public async Task<IEnumerable<Article>> GetByAuthorIdAsync(Guid authorId) => await _context.Articles.Where(a => a.AuthorId == authorId).ToListAsync();        

        public async Task<Article?> GetByIdAsync(Guid id) => await _context.Articles.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == id);

        public void Update(Article article) => _context.Articles.Update(article);
    }
}
