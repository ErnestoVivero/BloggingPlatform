using BloggingPlatform.Application.DTOs;
using BloggingPlatform.Application.Services;
using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Domain.Repositories;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Tests
{
    public class ArticleServiceTests
    {
        // Mocks
        private readonly Mock<IArticleRepository> _articleRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ArticleService _sut;

        public ArticleServiceTests() 
        {
            _articleRepositoryMock = new Mock<IArticleRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _sut = new ArticleService(_articleRepositoryMock.Object, _userRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenAuthorExists_ShouldReturnArticleResponseDtoAndSave()
        {
            //Arrange
            var authorId = Guid.NewGuid();
            var dto = new CreateArticleDto("Titulo test", "Contenido test", authorId);
            var fakeUser = new User("author_test", "test@test.com");

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId)).ReturnsAsync(fakeUser);

            //Act
            var result = await _sut.CreateAsync(dto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Title, result.Title);
            Assert.Equal(dto.Content, result.Content);
            Assert.False(result.IsPublished);

            _articleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Article>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenAuthorDoesNotExist_ShouldThrowKeyNotFoundExceptionAndNeverSave()
        {
            //Arrange
            var authorId = Guid.NewGuid();
            var dto = new CreateArticleDto("Titulo", "Contenido", authorId);

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId)).ReturnsAsync((User?)null);

            //Act & Assert
            //Capturar la excepcion
            var excepcion = await Assert.ThrowsAsync<KeyNotFoundException>(() => _sut.CreateAsync(dto));

            Assert.Contains($"El autor con ID {authorId} no fue encontrado", excepcion.Message);

            //Verificar que los repositorios de articulos y unit of work nunca se ejecutaron ya que fallo la validacion
            _articleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Article>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
