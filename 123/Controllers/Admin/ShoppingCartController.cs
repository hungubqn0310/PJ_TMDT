using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/shoppingCart")]
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(ILogger<ShoppingCartController> logger)
        {
            _logger = logger;
        }

        // Hiển thị danh sách các mục trong giỏ hàng
        public IActionResult Index()
        {
            var shoppingCartViewModel = new ShoppingCartViewModel();
            shoppingCartViewModel.ShoppingCarts = ShoppingCartService.GetCartItems();
            ViewBag.Users = UserService.GetUsers() ?? new List<User>();
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();

            return View("Views/Admin/shoppingcart.cshtml", shoppingCartViewModel);
        }

        // Hiển thị form thêm mới giỏ hàng
        [HttpGet("add")]
        public IActionResult Add()
        {
            ShoppingCart cartItem = new ShoppingCart();

            ViewBag.Users = UserService.GetUsers() ?? new List<User>(); 
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>(); 

            return PartialView("/Views/Admin/shoppingCartAdd.cshtml", cartItem);
        }

        // Thêm mới giỏ hàng
        [HttpPost("add")]
        public IActionResult Add(ShoppingCart cartItem)
        {
            ShoppingCartService.AddToCart(cartItem);
            return new RedirectResult("/admin/shoppingCart");
        }

        // Hiển thị form chỉnh sửa giỏ hàng
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var cartItem = ShoppingCartService.GetCartItemById(id);
            
            ViewBag.Users = UserService.GetUsers() ?? new List<User>(); 
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>(); 

            return PartialView("/Views/Admin/shoppingCartEdit.cshtml", cartItem);
        }

        // Chỉnh sửa giỏ hàng
        [HttpPost("edit")]
        public IActionResult Edit(ShoppingCart cartItem)
        {
            ShoppingCartService.UpdateCartItem(cartItem);
            return new RedirectResult("/admin/shoppingCart");
        }

        // Hiển thị form xóa giỏ hàng
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            var cartItem = ShoppingCartService.GetCartItemById(id);
            return PartialView("/Views/Admin/shoppingCartDelete.cshtml", cartItem);
        }

        // Xóa giỏ hàng
        [HttpPost("delete")]
        public IActionResult Delete(ShoppingCart cartItem)
        {
            ShoppingCartService.DeleteCartItem(cartItem.CartId);
            return new RedirectResult("/admin/shoppingCart");
        }

        // Xử lý lỗi
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
