using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.IServices;
using Assignment_2.Data;
using Assignment_2.Models;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            var categoryToUpdate = GetCategoryById(category.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            var categoryToDelete = GetCategoryById(id);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                _context.SaveChanges();
            }
        }
    }
}
