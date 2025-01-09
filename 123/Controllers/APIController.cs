using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Services;
using _123.Helpers;
namespace _123.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly ZaloPayService _zaloPayService;

        public ApiController(ILogger<ApiController> logger,ZaloPayService zaloPayService)
        {
            _logger = logger;
             _zaloPayService = zaloPayService;
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class OrderDto
        {
            public Order order { get; set; }
            public List<OrderItem> orderItems { get; set; }
            public int userId  { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest) {
            var res = AuthService.Login(loginRequest.Username, loginRequest.Password);

            if(res == null) {
                return NotFound(new ApiResponse<dynamic>(404, "Not Found", res));
            }  

            var response = new ApiResponse<dynamic>(200, "Success", res);
            return Ok(response);
            
        }

        [HttpGet("products/byId")]
        public IActionResult GetProductById([FromQuery] string id)
        {
            // Tạo một danh sách sản phẩm mẫu
            var products = ProductService.ViewGetProductById(id);

            // Kiểm tra nếu danh sách không rỗng
            if (products != null )
            {
              var response = new ApiResponse<dynamic>(200, "Success", products);
              return Ok(response);
            }
            else
            {
                // Nếu không có sản phẩm, trả về thông báo lỗi
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy sản phẩm nào."));
            }
        }

        [HttpGet("products/byCate")]
        public IActionResult GetProductByCate([FromQuery] int id)
        {
            // Tạo một danh sách sản phẩm mẫu
            var products = ProductService.ViewGetProductsByCateId(id);

            // Kiểm tra nếu danh sách không rỗng
            if (products != null )
            {
              var response = new ApiResponse<dynamic>(200, "Success", products);
              return Ok(response);
            }
            else
            {
                // Nếu không có sản phẩm, trả về thông báo lỗi
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy sản phẩm nào."));
            }
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            // Tạo một danh sách sản phẩm mẫu
            var products = ProductService.ViewGetProducts();

            // Kiểm tra nếu danh sách không rỗng
            if (products != null && products.Any())
            {
              var response = new ApiResponse<dynamic>(200, "Success", products);
              return Ok(response);
            }
            else
            {
                // Nếu không có sản phẩm, trả về thông báo lỗi
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy sản phẩm nào."));
            }
        }

        [HttpGet("categories")]
        public IActionResult GetCategorys()
        {
            // Tạo một danh sách sản phẩm mẫu
            var cates = CategoryService.ViewGetCategories();

            // Kiểm tra nếu danh sách không rỗng
            if (cates != null )
            {
              var response = new ApiResponse<dynamic>(200, "Success", cates);
              return Ok(response);
            }
            else
            {
                // Nếu không có sản phẩm, trả về thông báo lỗi
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy danh mục."));
            }
        }

        [HttpGet("carts")]
        public IActionResult GetCarts([FromQuery] int id)
        {
            // Tạo một danh sách sản phẩm mẫu
            var carts = ShoppingCartService.GetCartItemsByUserId(id);

            // Kiểm tra nếu danh sách không rỗng
            if (carts != null )
            {
              var response = new ApiResponse<dynamic>(200, "Success", carts);
              return Ok(response);
            }
            else
            {
                // Nếu không có sản phẩm, trả về thông báo lỗi
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy rỏ hàng."));
            }
        }

        [HttpPost("cart")]
        public IActionResult AddorUpdateCart([FromBody] ShoppingCart cart)
        {
            // Tạo một danh sách sản phẩm mẫu
            var carts = ShoppingCartService.AddOrUpdateToCart(cart);

            // Kiểm tra nếu danh sách không rỗng
            if (carts != null )
            {
              var response = new ApiResponse<dynamic>(200, "Success", carts);
              return Ok(response);
            }
            else
            {
                // Nếu không có sản phẩm, trả về thông báo lỗi
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy rỏ hàng."));
            }
        }

        [HttpPost("carts")]
        public IActionResult AddorUpdateCarts([FromBody] ShoppingCart[] carts)
        {
            Console.WriteLine(carts);
            // Tạo một danh sách sản phẩm mẫu
            var mcarts = ShoppingCartService.AddOrUpdateToManyCart(carts);

            // Kiểm tra nếu danh sách không rỗng
            if (mcarts != null )
            {
              var response = new ApiResponse<dynamic>(200, "Success", mcarts);
              return Ok(response);
            }
            else
            {
                // Nếu không có sản phẩm, trả về thông báo lỗi
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy rỏ hàng."));
            }
        }

        [HttpDelete("cart")]
        public IActionResult RemoveFromCart([FromQuery] int cartId)
        {
            var result = ShoppingCartService.DeleteCartItem(cartId);
            if (result >0)
            {
                return Ok(new ApiResponse<dynamic>(200, "Sản phẩm đã được xóa khỏi giỏ hàng"));
            }
            else
            {
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy sản phẩm trong giỏ hàng"));
            }
        }

        [HttpPost("orders")]
        public IActionResult CreateOrder([FromBody] OrderDto orders)
        {
            var result = OrderService.CreateOrder(orders.order);
            if (result >0)
            {
                var orderItems = OrderItemService.CreateOrderItems(
                    orders.orderItems.Select(item => new OrderItem { OrderId = result, ProductName= item.ProductName, Quantity = item.Quantity, Price = item.Price}).ToList());
                var shop = ShoppingCartService.UpdateCartToDeleteByUserId(orders.userId);
                return Ok(new ApiResponse<dynamic>(200, "Tao don hang thanh cong"));
            }
            else
            {
                return NotFound(new ApiResponse<dynamic>(404, "Tao don hang that bai"));
            }
        }
        [HttpGet("orders")]
        public IActionResult GetOrdersByUserId([FromQuery] int userId)
        {
            // Lấy danh sách đơn hàng theo userId
            var orders = OrderService.GetOrdersByUserId(userId);

            // Kiểm tra nếu danh sách không rỗng
            if (orders != null && orders.Count > 0)
            {
                var response = new ApiResponse<dynamic>(200, "Success", orders);
                return Ok(response);
            }
            else
            {
                // Nếu không có đơn hàng, trả về thông báo lỗi
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy đơn hàng."));
            }
        }
        [HttpPost("orders/cancel")]
        public IActionResult CancelOrder([FromQuery] int orderId)
        {
            // Gọi service để hủy đơn hàng
            var result = OrderService.CancelOrder(orderId);

            if (result > 0)
            {
                return Ok(new ApiResponse<dynamic>(200, "Đơn hàng đã được hủy thành công."));
            }
            else
            {
                return NotFound(new ApiResponse<dynamic>(404, "Không tìm thấy đơn hàng hoặc hủy thất bại."));
            }
        }

        public class PaymentRequest
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        [HttpPost("zalo1")]
        public async Task<IActionResult> CreatePaymentZalo([FromBody] PaymentRequest request)
        {
            try
            {
                // Validate request
                // if (request == null || string.IsNullOrWhiteSpace(request.OrderId) || 
                //     request.Amount <= 0 || string.IsNullOrWhiteSpace(request.UserPhone))
                // {
                //     return BadRequest(new { message = "Dữ liệu không hợp lệ" });
                // }

                // Gọi dịch vụ thanh toán
                var result = await ZaloPayService.CreatePaymentRequestAsync(request.Amount, request.Description);

                // Trả về kết quả dưới dạng HTTP Response
                return Ok(new { message = "Thanh toán thành công", data = result });
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, trả về mã lỗi HTTP với thông điệp lỗi
                return BadRequest(new { message = "Lỗi: " + ex.Message });
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}