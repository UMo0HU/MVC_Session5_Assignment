using Assignment_2.Data;
using Assignment_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.IServices;

namespace Assignment_2.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            ViewData["Categories"] = _categoryService.GetAllCategories();
            return View();
        }

        public IActionResult CreateForm()
        {
            return View();
        }

        public IActionResult CreateCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _categoryService.AddCategory(category);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the category: " + ex.Message);
                }
            }
            return View("CreateForm", category);
        }

        public IActionResult EditForm(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            return View(category);
        }

        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _categoryService.UpdateCategory(category);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the category: " + ex.Message);
                }
            }
            return View("EditForm", category);
        }

        public IActionResult DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            ViewBag.Category = _categoryService.GetCategoryById(id);
            return View();
        }
    }
}
