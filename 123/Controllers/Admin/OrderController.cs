using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/order")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        // Index Action: Display all orders
        public IActionResult Index()
        {
            var orderViewModel = new OrderViewModel();
            orderViewModel.Orders = OrderService.GetOrders();
            ViewBag.Users = UserService.GetUsers() ?? new List<User>();
             // Fetch orders from service
            // Return the orders view
            return View("Views/Admin/order.cshtml", orderViewModel);
        }

        // GET Action: Show the Add Order Form
        [HttpGet("add")]
        public IActionResult Add()
        {
            Order order = new Order();
            ViewBag.Users = UserService.GetUsers() ?? new List<User>(); 

            return PartialView("/Views/Admin/orderadd.cshtml", order);
        }

        // POST Action: Add a new order
        [HttpPost("add")]
        public IActionResult Add(Order order)
        {
            OrderService.CreateOrder(order); // Call service to create order
            return new RedirectResult("/admin/order");
        }

        // GET Action: Show the Edit Order Form
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            Order order = OrderService.GetOrderById(id); 
            ViewBag.Users = UserService.GetUsers() ?? new List<User>(); 
            // Fetch order by ID
            return PartialView("/Views/Admin/orderedit.cshtml", order);
        }

        // POST Action: Update order details
        [HttpPost("edit")]
        public IActionResult Edit(Order order)
        {
            OrderService.UpdateOrder(order); // Call service to update order
            return new RedirectResult("/admin/order");
        }

        // GET Action: Show the Delete Order Confirmation
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Order order = OrderService.GetOrderById(id); // Fetch order by ID
            return PartialView("/Views/Admin/orderdelete.cshtml", order);
        }

        // POST Action: Delete the order
        [HttpPost("delete")]
        public IActionResult Delete(Order order)
        {
            OrderService.DeleteOrder(order.OrderId); // Call service to delete order
            return new RedirectResult("/admin/order");
        }

        // Error Handling Action
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
