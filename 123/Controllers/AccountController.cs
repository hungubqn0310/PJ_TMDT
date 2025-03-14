using Microsoft.AspNetCore.Mvc;
using _123.Models;
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
        public IActionResult SignUp(Users userAccount)
        {
            if (ModelState.IsValid)
            {
                string result = AuthService.Signup(userAccount); // Không dùng await

                if (result == "Đăng ký thành công.")
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", result);
            }
            return View(userAccount);
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không được để trống.");
                return View();
            }

            var userAccount = AuthService.Login(username, password);

            if (userAccount != null)
            {
                TempData["SuccessMessage"] = "Đăng nhập thành công!";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View();
        }
    }
}
