using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_2.Models;

namespace Services.IServices
{
    public interface ICustomerService
    {
        public List<Customer> GetCustomers();
    }
}
