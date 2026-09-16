using ETAPI.Models;
using ETAPI.Repositories;

namespace ETAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            return await _categoryRepository.CreateAsync(category);
        }

        public async Task<bool> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory is null)
            {
                return false;
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            await _categoryRepository.UpdateAsync(existingCategory);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return false;
            }
            await _categoryRepository.DeleteAsync(category);
            return true;
        }
    }
}