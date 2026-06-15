using BloggingPlatform.Application.DTOs;
using BloggingPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleDto dto)
        {
            try
            {
                var result = await _articleService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _articleService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("{id:guid}/publish")]
        public async Task<IActionResult> Publish(Guid id)
        {
            try
            {
                await _articleService.PublishAsync(id);
                return NoContent(); // Respuesta estándar 204 para modificaciones exitosas sin cuerpo de retorno
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
