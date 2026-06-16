using BloggingPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Tests.Domain
{
    public class ArticleTests
    {
        [Fact]
        public void Article_CreateNewArticle_ShouldBeCreatedAsUnpublished()
        {
            //Arrange
            var articleId = Guid.NewGuid();

            //Act
            var article = new Article("Titulo", "Contenido", articleId);

            //Assert
            Assert.False(article.IsPublished);
            Assert.Null(article.PublishedAt);
            Assert.NotEqual(Guid.Empty, article.Id);
        }

        [Fact]
        public void Article_WhenArticleIsPublished_ShouldSetIsPublishedToTrueAndSetPublishedAt()
        {
            //Arrange
            var articleId = Guid.NewGuid();
            var article = new Article("Titulo", "Contenido", articleId);

            //Act
            article.Publish();

            //Assert
            Assert.True(article.IsPublished);
            Assert.NotNull(article.PublishedAt);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NewArticle_WhenTitleIsNullOrEmptyOrWhitespace_ShouldThrowArgumentException(string? invalidTitle)
        {
            //Arrange
            var authorId = Guid.NewGuid();
            var validContent = "Contenido válido del artículo";

            //Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Article(invalidTitle, validContent, authorId));

            Assert.Contains("El título no puede estar vacío", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NewArticle_WhenContentIsNullOrEmptyOrWhitespace_ShouldThrowArgumentException(string? invalidContent)
        {
            //Arrange
            var authorId = Guid.NewGuid();
            var validTitle = "Titulo valido";

            //Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Article(validTitle, invalidContent, authorId));

            Assert.Contains("El contenido no puede estar vacío",exception.Message);
        }

        [Fact]
        public void NewArticle_WhenAuthorIdIsEmpty_ShouldThrowArgumentException()
        {
            //Arrange
            var authorId = Guid.Empty;

            //Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Article("Titulo","Contenido",authorId));

            Assert.Contains("El articulo debe tener un autor valido", exception.Message);
        }

        [Fact]
        public void Publish_WhenArticleIsAlreadyPublished_ShouldThrowInvalidOperationException()
        {
            //Arrange
            var article = new Article("Título Válido", "Contenido Válido", Guid.NewGuid());
            article.Publish();

            //Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => article.Publish());

            Assert.Contains("El artículo ya se encuentra publicado", exception.Message);
        }

        [Fact]
        public void Unpublish_WhenArticleIsUnpublished_ShouldSetIsPublishedToFalseAndSetUpdateAt()
        {
            //Arrange
            var articleId = Guid.NewGuid();
            var article = new Article("Titulo", "Contenido", articleId);

            article.Publish();

            //Act
            article.Unpublish();

            //Assert
            Assert.False(article.IsPublished);
            Assert.NotNull(article.UpdatedAt);
        }
    }
}
