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
            Console.WriteLine(id);
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


        [HttpPost("zalo")]
        public async Task<IActionResult> CreateOrder([FromQuery] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Amount must be greater than 0.");
            }

            try
            {
                var response = await _zaloPayService.CreateZaloPayOrder(amount);
            Console.WriteLine(response.ReturnCode);

                if (response.ReturnCode == 1)
                {
                    return Ok(new { OrderUrl = response.OrderUrl });
                }
                else
                {
                    return BadRequest(new { Message = response.ReturnMessage });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error creating ZaloPay order: {ex.Message}" });
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}