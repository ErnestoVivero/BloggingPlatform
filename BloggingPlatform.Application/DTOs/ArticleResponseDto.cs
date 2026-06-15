using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Application.DTOs
{
    public record ArticleResponseDto
    (
        Guid Id,
        string Title,
        string Content,
        bool IsPublished,
        DateTime? PublishedAt,
        DateTime CreatedAt,
        Guid AuthorId
    );
}
