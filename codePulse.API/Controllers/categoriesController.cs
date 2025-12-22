using codePulse.AI.Data;
using codePulse.AI.Models.Domain;
using codePulse.API.Models.DTO;
using codePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase

    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            //Map request DTO to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);

            //Domain to model
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] string? query, 
            [FromQuery] string? sortBy, 
            [FromQuery] string? sortDirection)
        {
            var categories = await categoryRepository.GetAllAsync(query, sortBy, sortDirection);

            var response = categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            });

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto request)
        {
            var category = new Category
            {
                Id= id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            var updatedCategory = await categoryRepository.UpdateAsync(category);   

            if (updatedCategory == null)
            {
                return NotFound();
            }

            //Domain to DTO
            var response = new CategoryDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                UrlHandle = updatedCategory.UrlHandle
            };  
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var deletedCategory = await categoryRepository.DeleteAsync(id);
            if (deletedCategory == null)
            {
                return NotFound();
            }
            
            var response = new CategoryDto
            {
                Id = deletedCategory.Id,
                Name = deletedCategory.Name,
                UrlHandle = deletedCategory.UrlHandle
            };  
            return Ok(response);
        }
    }
}
