using Assignment_2.Data;
using Assignment_2.Models;
using Microsoft.EntityFrameworkCore;
using Services.IServices;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductService(AppDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public List<Product> GetAllProductsWithCategory()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            var fileName = string.Empty;
            if (product.ClientFile != null)
            {
                string myUpload = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                fileName = product.ClientFile.FileName;
                string fullPath = Path.Combine(myUpload, fileName);
                product.ClientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                product.Img = fileName;
            }
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var productToUpdate = GetProductById(product.Id);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Description = product.Description;
                if (product.ClientFile != null)
                {
                    string myUpload = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    string fullPath = Path.Combine(myUpload, product.ClientFile.FileName);
                    product.ClientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    productToUpdate.Img = product.ClientFile.FileName;
                }
                productToUpdate.CategoryId = product.CategoryId;
                _context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var productToDelete = GetProductById(id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
            }
        }
    }
}
