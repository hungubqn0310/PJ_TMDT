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

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}