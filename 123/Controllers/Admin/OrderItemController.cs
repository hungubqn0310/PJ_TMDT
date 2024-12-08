using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/order-item")]
    public class OrderItemController : Controller
    {
        private readonly ILogger<OrderItemController> _logger;

        public OrderItemController(ILogger<OrderItemController> logger)
        {
            _logger = logger;
        }

        // Index Action: Display all order items
        public IActionResult Index()
        {
            var orderItemViewModel = new OrderItemViewModel();
            orderItemViewModel.Order_Items = OrderItemService.GetAllOrderItems(); // Fetch order items from service
            // Return the order items view
            return View("Views/Admin/orderitem.cshtml", orderItemViewModel);
        }

        // GET Action: Show the Add Order Item Form
        [HttpGet("add")]
        public IActionResult Add()
        {
            Order_Item orderItem = new Order_Item();
            return PartialView("/Views/Admin/orderitemadd.cshtml", orderItem);
        }

        // POST Action: Add a new order item
        [HttpPost("add")]
        public IActionResult Add(Order_Item orderItem)
        {
            OrderItemService.CreateOrderItem(orderItem); // Call service to create order item
            return new RedirectResult("/admin/order-item");
        }

        // GET Action: Show the Edit Order Item Form
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            Order_Item orderItem = OrderItemService.GetOrderItemById(id); // Fetch order item by ID
            return PartialView("/Views/Admin/orderitemedit.cshtml", orderItem);
        }

        // POST Action: Update order item details
         // POST Action: Update order item details
        [HttpPost("edit")]
        public IActionResult Edit(Order_Item orderItem)
        {
            OrderItemService.UpdateOrderItem(orderItem); // Call service to update order item
            return new RedirectResult("/admin/order-item");
        }

        // GET Action: Show the Delete Order Item Confirmation
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Order_Item orderItem = OrderItemService.GetOrderItemById(id); // Fetch order item by ID
            return PartialView("/Views/Admin/orderitemdelete.cshtml", orderItem);
        }

        // POST Action: Delete the order item
        [HttpPost("delete")]
        public IActionResult Delete(Order_Item orderItem)
        {
            OrderItemService.DeleteOrderItem(orderItem.order_item_id); // Call service to delete order item
            return new RedirectResult("/admin/order-item");
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
