using codePulse.AI.Data;
using codePulse.AI.Models.Domain;
using codePulse.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {

    private readonly ApplicationDbContext dbContext;
    public CategoriesController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
  
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            //Map request DTO to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            //Domain to model
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            }; 

            return Ok(response);
        }
    }
}
