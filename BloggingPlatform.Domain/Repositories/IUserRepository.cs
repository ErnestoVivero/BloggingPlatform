using BloggingPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        void Update(User user);
    }
}
