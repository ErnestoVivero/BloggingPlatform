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
    }
}
