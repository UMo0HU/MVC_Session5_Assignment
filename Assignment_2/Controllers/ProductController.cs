using Assignment_2.Models;
using Assignment_2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace Assignment_2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
        }

        public IActionResult Index()
        {
            ProductsViewModel productsViewModel = new ProductsViewModel();
            // GetAllProductsWithCategory
            productsViewModel.products = _productService.GetAllProductsWithCategory();
            return View(productsViewModel);
        }

        public IActionResult CreateForm()
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View();
        }


        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productService.AddProduct(product);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the product: " + ex.Message);
                }
            }
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View("CreateForm", product);
        }

        public IActionResult EditForm(int id)
        {
            var product = _productService.GetProductById(id);
            ViewBag.categories = _categoryService.GetAllCategories();
            return View(product);
        }

        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productService.UpdateProduct(product);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the product: " + ex.Message);
                }
            }
            ViewBag.categories = _categoryService.GetAllCategories();
            return View("EditForm", product);
        }

        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            ViewBag.Product = _productService.GetProductById(id);
            return View();
        }
    }
}
