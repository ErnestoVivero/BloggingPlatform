using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Domain.Repositories;
using BloggingPlatform.Infrastructure.Pertistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

        public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) => await _context.Users.FindAsync(id);        

        public void Update(User user) => _context.Users.Update(user);
        
    }
}
