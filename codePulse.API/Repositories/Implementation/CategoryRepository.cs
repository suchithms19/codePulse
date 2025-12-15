using codePulse.AI.Data;
using codePulse.AI.Models.Domain;
using codePulse.API.Repositories.Interface;

namespace codePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {   
        private readonly ApplicationDbContext dbContext;
        public CategoryRepository(ApplicationDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
    }
}
