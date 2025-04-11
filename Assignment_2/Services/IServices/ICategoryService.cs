using Assignment_2.Models;

namespace Services.IServices
{
    public interface ICategoryService
    {
        public List<Category> GetAllCategories();
        public Category GetCategoryById(int id);
        public void AddCategory(Category category);
        public void UpdateCategory(Category category);
        public void DeleteCategory(int id);
    }
}
