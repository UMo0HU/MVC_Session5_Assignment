using Assignment_2.Data;
using Assignment_2.Models;
using Assignment_2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.IServices;

namespace Assignment_2.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(AppDbContext context,
            ICustomerService customerService,
            IOrderService orderService,
            IProductService productService)
        {
            this._context = context;
            this._customerService = customerService;
            this._orderService = orderService;
            this._productService = productService;
        }

        public IActionResult Index()
        {
            var ordersCustomersViewModel = _orderService.GetAllOrdersWithCustomers();
            return View(ordersCustomersViewModel);
        }

        public IActionResult CreateForm()
        {
            ViewData["Customers"] = _customerService.GetCustomers();
            ViewData["Products"] = _productService.GetAllProductsWithCategory();
            return View();
        }

        public IActionResult CreateOrder(Order order, List<OrderItem> orderItems)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _orderService.AddOrder(order, orderItems);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the order: " + ex.Message);
                }
            }
            ViewData["Customers"] = _customerService.GetCustomers();
            ViewData["Products"] = _productService.GetAllProductsWithCategory();
            return View("CreateForm");
        }

        public IActionResult EditForm(int id)
        {
            var order = _orderService.GetOrderById(id);
            ViewData["OrderItems"] = order.OrderItems.ToList();
            ViewData["Customers"] = _customerService.GetCustomers();
            ViewData["Products"] = _productService.GetAllProductsWithCategory();
            return View(order);
        }

        public IActionResult EditOrder(int id, List<OrderItem> orderItems)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _orderService.UpdateOrder(id, orderItems);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the order: " + ex.Message);
                }
            }
            var order = _orderService.GetOrderById(id);
            ViewData["OrderItems"] = order.OrderItems.ToList();
            ViewData["Customers"] = _customerService.GetCustomers();
            ViewData["Products"] = _productService.GetAllProductsWithCategory();
            return View("EditForm", order);
        }

        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var orderOderItemProductViewModel = _orderService.GetOrderWithDetails(id);
            return View(orderOderItemProductViewModel);
        }
    }
}
