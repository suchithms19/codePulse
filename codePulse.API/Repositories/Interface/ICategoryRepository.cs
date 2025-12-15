using codePulse.AI.Models.Domain;

namespace codePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetCategoryAsync(Guid id);
    }
}
