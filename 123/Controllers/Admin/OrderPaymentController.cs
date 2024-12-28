using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/order-payment")]
    public class OrderPaymentController : Controller
    {
        private readonly ILogger<OrderPaymentController> _logger;

        public OrderPaymentController(ILogger<OrderPaymentController> logger)
        {
            _logger = logger;
        }

        // Index Action: Display all order payments
        public IActionResult Index()
        {
            var orderPaymentViewModel = new OrderPaymentViewModel();
            orderPaymentViewModel.OrderPayments = OrderPaymentService.GetOrderPayments(); // Fetch order payments from service
            
            ViewBag.Orders = OrderService.GetOrders() ?? new List<Order>();
            ViewBag.PaymentMethods = PaymentMethodService.GetPaymentMethods() ?? new List<PaymentMethod>();

            return View("Views/Admin/orderpayment.cshtml", orderPaymentViewModel); // Return the order payments view
        }

        // GET Action: Show the Add Order Payment Form
        [HttpGet("add")]
        public IActionResult Add()
        {
            OrderPayment orderPayment = new OrderPayment();

             ViewBag.Orders = OrderService.GetOrders() ?? new List<Order>();
            ViewBag.PaymentMethods = PaymentMethodService.GetPaymentMethods() ?? new List<PaymentMethod>();

            return PartialView("/Views/Admin/orderpaymentadd.cshtml", orderPayment); // Return add view for order payment
        }

        // POST Action: Add a new order payment
        [HttpPost("add")]
        public IActionResult Add(OrderPayment orderPayment)
        {
            OrderPaymentService.CreateOrderPayment(orderPayment); // Call service to create order payment
            return new RedirectResult("/admin/order-payment");
        }

        // GET Action: Show the Edit Order Payment Form
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            OrderPayment orderPayment = OrderPaymentService.GetOrderPaymentById(id); // Fetch order payment by ID
            
             ViewBag.Orders = OrderService.GetOrders() ?? new List<Order>();
            ViewBag.PaymentMethods = PaymentMethodService.GetPaymentMethods() ?? new List<PaymentMethod>();

            return PartialView("/Views/Admin/orderpaymentedit.cshtml", orderPayment); // Return edit view for order payment
        }

        // POST Action: Update order payment details
        [HttpPost("edit")]
        public IActionResult Edit(OrderPayment orderPayment)
        {
            OrderPaymentService.UpdateOrderPayment(orderPayment); // Call service to update order payment
            return new RedirectResult("/admin/order-payment");
        }

        // GET Action: Show the Delete Order Payment Confirmation
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            OrderPayment orderPayment = OrderPaymentService.GetOrderPaymentById(id); // Fetch order payment by ID
            return PartialView("/Views/Admin/orderpaymentdelete.cshtml", orderPayment); // Return delete confirmation view
        }

        // POST Action: Delete the order payment
        [HttpPost("delete")]
        public IActionResult Delete(OrderPayment orderPayment)
        {
            OrderPaymentService.DeleteOrderPayment(orderPayment.OrderPaymentId); // Call service to delete order payment
            return new RedirectResult("/admin/order-payment");
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
