using InspirePO.Models;

namespace InspirePO.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<FormCategory>> GetAllCategories();
        Task<FormCategory?> GetCategoryById(long id);
        Task<FormCategory> AddCategory(FormCategory category);
        Task<bool> DeleteCategory(long id);
        Task<bool> UpdateCategory(long id, FormCategory category);
    }
}
