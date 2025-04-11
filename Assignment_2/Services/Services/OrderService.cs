using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.IServices;
using Assignment_2.Data;
using Assignment_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Assignment_2.ViewModels;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrderById(id);
            _context.OrderItems.RemoveRange(order.OrderItems);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public OrderOderItemProductViewModel GetOrderWithDetails(int id)
        {
            var orderOderItemProductViewModel = new OrderOderItemProductViewModel();
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            orderOderItemProductViewModel.Order = order;
            orderOderItemProductViewModel.orderItems = order.OrderItems.ToList();
            orderOderItemProductViewModel.products = order.OrderItems.Select(oi => oi.Product).ToList();
            return orderOderItemProductViewModel;
        }

        public OrdersCustomersViewModel GetAllOrdersWithCustomers()
        {
            var ordersCustomersViewModel = new OrdersCustomersViewModel();
            ordersCustomersViewModel.orders = GetOrders();
            ordersCustomersViewModel.customers = _context.Customers.ToList();
            return ordersCustomersViewModel;
        }

        public void AddOrder(Order order, List<OrderItem> orderItems)
        {
            decimal total = 0;
            var newOrder = new Order
            {
                CustomerId = order.CustomerId,
                OrderDate = DateTime.Now,
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            foreach (var orderItem in orderItems)
            {
                orderItem.Product = _context.Products.FirstOrDefault(p => p.Id == orderItem.ProductId);
                var newOrderItem = new OrderItem
                {
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    Price = (decimal)(orderItem.Product.Price * orderItem.Quantity),
                    OrderId = newOrder.Id
                };

                total += newOrderItem.Price;
                _context.OrderItems.Add(newOrderItem);
            }

            _context.SaveChanges();

            var orderToUpdate = _context.Orders.FirstOrDefault(o => o.Id == newOrder.Id);
            orderToUpdate.TotalAmount = total;
            _context.SaveChanges();
        }

        public void UpdateOrder(int id, List<OrderItem> orderItems)
        {
            decimal total = 0;
            var orderToUpdate = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);

            foreach (var orderItem in orderItems)
            {
                orderItem.Product = _context.Products.FirstOrDefault(p => p.Id == orderItem.ProductId);
                var orderItemToUpdate = orderToUpdate.OrderItems.FirstOrDefault(oi => oi.Id == orderItem.Id && oi.ProductId == orderItem.ProductId);
                orderItemToUpdate.Quantity = orderItem.Quantity;
                orderItemToUpdate.Price = (decimal)(orderItem.Product.Price * orderItem.Quantity);
                total += orderItemToUpdate.Price;

                _context.OrderItems.Update(orderItemToUpdate);
            }

            orderToUpdate.TotalAmount = total;
            _context.Orders.Update(orderToUpdate);
            _context.SaveChanges();    
        }
    }
}
