using codePulse.AI.Data;
using codePulse.AI.Models.Domain;
using codePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Category>> GetAllAsync(string? query=null,
                string? sortBy=null,
                string? sortDirection=null,
                int? pageNumber = 1,
                int? pageSize = 100)
        {
            var categories = dbContext.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                categories = categories.Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection,"asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(x=>x.Name): categories.OrderByDescending(x=>x.Name);
                }

                if (sortBy.Equals("URL", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(x => x.UrlHandle) : categories.OrderByDescending(x => x.UrlHandle);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;
            categories = categories.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (existingCategory == null)
            {
                return null;
            }

            dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory == null)
            {
                return null;
            }
            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
