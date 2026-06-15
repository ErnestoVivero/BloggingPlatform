using BloggingPlatform.Application.DTOs;
using BloggingPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Application.Mapping
{
    public static class ArticleMappingExtensions
    {
        public static ArticleResponseDto ToResponseDto(this Article article)
        {
            return new ArticleResponseDto(
                article.Id,
                article.Title,
                article.Content,
                article.IsPublished,
                article.PublishedAt,
                article.CreatedAt,
                article.AuthorId
            );
        }
    }
}
