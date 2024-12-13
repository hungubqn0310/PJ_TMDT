using Microsoft.AspNetCore.Mvc;
using _123.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using _123.Services;

namespace _123.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /Account/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: /Account/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(Users userAccount)
        {
            if (ModelState.IsValid)
            {
                // Thực hiện đăng ký
                var st = AuthService.Signup(userAccount);

                // Thêm thông báo đăng ký thành công
                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";

                // Chuyển hướng đến trang Home
                return RedirectToAction("Index", "Home");
            }
            return View(userAccount);
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var userAccount = AuthService.Login(username, password);

            if (userAccount != null)
            {
                // Đăng nhập thành công
                return RedirectToAction("Index", "Home");
            }

            // Đăng nhập thất bại
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View();
        }

    }
}
