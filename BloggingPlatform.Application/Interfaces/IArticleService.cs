using BloggingPlatform.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Application.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleResponseDto?> CreateAsync(CreateArticleDto dto);
        Task<ArticleResponseDto?> GetByIdAsync(Guid id);
        Task PublishAsync(Guid id);
    }
}
