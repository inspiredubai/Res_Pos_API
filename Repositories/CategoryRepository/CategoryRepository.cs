using InspirePO.Data;
using InspirePO.Models;
using InspirePO.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspirePO.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<FormCategory> AddCategory(FormCategory category)
        {
            try
            {
                if (category.CategoryId == 0)
                {
                    category.CategoryId = (_context.FormCategories.Max(c => (long?)c.CategoryId) ?? 0) + 1;
                }
                _context.FormCategories.Add(category);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Category added successfully with ID: {CategoryId}", category.CategoryId);
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding category");
                throw;
            }
        }

        public async Task<bool> DeleteCategory(long id)
        {
            try
            {
                var category = await _context.FormCategories.FindAsync(id);
                if (category != null)
                {
                    _context.FormCategories.Remove(category);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Category deleted successfully with ID: {CategoryId}", id);
                    return true;
                }

                _logger.LogWarning("Category not found with ID: {CategoryId}", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting category with ID: {CategoryId}", id);
                throw;
            }
        }

        public async Task<List<FormCategory>> GetAllCategories()
        {
            try
            {
                var categories = await _context.FormCategories.ToListAsync();
                _logger.LogInformation("Retrieved {Count} categories", categories.Count);
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving categories");
                throw;
            }
        }

        public async Task<FormCategory?> GetCategoryById(long id)
        {
            try
            {
                var category = await _context.FormCategories.FindAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category not found with ID: {CategoryId}", id);
                    return null;
                }
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving category with ID: {CategoryId}", id);
                throw;
            }
        }

        public async Task<bool> UpdateCategory(long id, FormCategory categoryNew)
        {
            try
            {
                var categoryOld = await _context.FormCategories.FindAsync(id);

                if (categoryOld == null)
                {
                    _logger.LogWarning("Category not found with ID: {CategoryId} for update", id);
                    return false;
                }

                categoryOld.Category = categoryNew.Category;

                _context.Entry(categoryOld).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Category updated successfully with ID: {CategoryId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating category with ID: {CategoryId}", id);
                throw;
            }
        }
    }

}
