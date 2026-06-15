using BloggingPlatform.Application.DTOs;
using BloggingPlatform.Application.Interfaces;
using BloggingPlatform.Application.Mapping;
using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository, IUnitOfWork unitOfWork) 
        { 
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleResponseDto?> CreateAsync(CreateArticleDto dto)
        {
            // 1. Validar que el autor realmente exista antes de asignarle un post
            var author = await _userRepository.GetByIdAsync(dto.AuthorId);

            if (author == null)
                throw new KeyNotFoundException($"El autor con ID {dto.AuthorId} no fue encontrado");

            // 2. Crear la entidad de dominio utilizando su constructor rico (el cual auto-valida sus reglas)
            var article = new Article(dto.Title, dto.Content, dto.AuthorId);

            // 3. 
            await _articleRepository.AddAsync(article);

            await _unitOfWork.SaveChangesAsync();

            return article.ToResponseDto();
        }

        public async Task<ArticleResponseDto?> GetByIdAsync(Guid id)
        {
            var article = await _articleRepository.GetByIdAsync(id);

            if (article == null) return null;

            return article.ToResponseDto();
        }

        public async Task PublishAsync(Guid id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
            {
                throw new KeyNotFoundException($"El artículo con ID {id} no existe.");
            }

            // Delegamos la regla de negocio a la propia entidad (Modelo Rico)
            article.Publish();

            // Le avisamos al repositorio que rastree este cambio
            _articleRepository.Update(article);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
