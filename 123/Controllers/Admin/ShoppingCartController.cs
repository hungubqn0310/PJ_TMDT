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
            shoppingCartViewModel.Shopping_Carts = ShoppingCartService.GetCartItems();
            // Gửi dữ liệu đến View
            return View("Views/Admin/shoppingcart.cshtml", shoppingCartViewModel);
        }

        // Hiển thị form thêm mới giỏ hàng
        [HttpGet("add")]
        public IActionResult Add()
        {
            var cartItem = new Shopping_Cart();
            var users = UserService.GetUsers();
            var model = new ShoppingCartViewModel();
            model.Shopping_Cart = cartItem;
            model.Users = users;
            return PartialView("/Views/Admin/cartadd.cshtml", model);
        }

        // Thêm mới giỏ hàng
        [HttpPost("add")]
        public IActionResult Add(Shopping_Cart cartItem)
        {
            ShoppingCartService.AddToCart(cartItem);
            return new RedirectResult("/admin/shoppingCart");
        }

        // Hiển thị form chỉnh sửa giỏ hàng
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var cartItem = ShoppingCartService.GetCartItemById(id);
            return PartialView("/Views/Admin/cartedit.cshtml", cartItem);
        }

        // Chỉnh sửa giỏ hàng
        [HttpPost("edit")]
        public IActionResult Edit(Shopping_Cart cartItem)
        {
            ShoppingCartService.UpdateCartItem(cartItem);
            return new RedirectResult("/admin/shoppingCart");
        }

        // Hiển thị form xóa giỏ hàng
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var cartItem = ShoppingCartService.GetCartItemById(id);
            return PartialView("/Views/Admin/cartdelete.cshtml", cartItem);
        }

        // Xóa giỏ hàng
        [HttpPost("delete")]
        public IActionResult Delete(Shopping_Cart cartItem)
        {
            Console.WriteLine(cartItem.cart_id);
            ShoppingCartService.DeleteCartItem(cartItem.cart_id);
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
