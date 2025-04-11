using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_2.Models;

namespace Services.IServices
{
    public interface IProductService
    {
        public List<Product> GetAllProductsWithCategory();
        public void AddProduct(Product product);
        public Product GetProductById(int id);
        public void UpdateProduct(Product product);
        public void DeleteProduct(int id);

    }
}
