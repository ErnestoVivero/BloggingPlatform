using BloggingPlatform.Domain.Repositories;
using BloggingPlatform.Infrastructure.Pertistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
