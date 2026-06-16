using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Domain.Entities
{
    public class Article
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public bool IsPublished { get; private set; }
        public DateTime? PublishedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Guid AuthorId { get; private set; }
        public User Author { get; private set; } = null!;

        private Article() { }

        public Article(string? title, string? content, Guid authorId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("El título no puede estar vacío", nameof(title));

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("El contenido no puede estar vacío", nameof(content));

            if(authorId == Guid.Empty)
                throw new ArgumentException("El articulo debe tener un autor valido", nameof(authorId));

            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            AuthorId = authorId;
            IsPublished = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void Publish()
        {
            if (IsPublished)
                throw new InvalidOperationException("El artículo ya se encuentra publicado.");

            IsPublished = true;
            PublishedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Unpublish()
        {
            if (!IsPublished)
                return;

            IsPublished = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateContent(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Título y contenido son obligatorios para actualizar.");

            Title = title;
            Content = content;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
