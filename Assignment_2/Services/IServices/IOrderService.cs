using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_2.Models;
using Assignment_2.ViewModels;

namespace Services.IServices
{
    public interface IOrderService
    {
        public List<Order> GetOrders();
        public Order GetOrderById(int id);
        public void DeleteOrder(int id);
        public OrderOderItemProductViewModel GetOrderWithDetails(int id);
        public OrdersCustomersViewModel GetAllOrdersWithCustomers();
        public void AddOrder(Order order, List<OrderItem> orderItems);
        public void UpdateOrder(int id, List<OrderItem> orderItems);



    }
}
