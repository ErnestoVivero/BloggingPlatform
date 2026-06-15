using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
