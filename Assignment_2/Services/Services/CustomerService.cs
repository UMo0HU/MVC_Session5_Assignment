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
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }
        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }
    }
}
