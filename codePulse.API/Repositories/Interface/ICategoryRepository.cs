using codePulse.AI.Models.Domain;

namespace codePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync(string? query=null, 
            string? sortBy=null,
            string? sortDirection=null);

        Task<Category?> GetCategoryAsync(Guid id);

        Task<Category?> UpdateAsync(Category category);

        Task<Category?> DeleteAsync(Guid id);
    }
}
