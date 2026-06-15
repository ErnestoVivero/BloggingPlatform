using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Application.DTOs
{
    public record CreateArticleDto
    (
        string Title,
        string Content,
        Guid AuthorId
    );
}