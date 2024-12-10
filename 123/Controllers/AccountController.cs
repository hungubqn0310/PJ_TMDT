// using Microsoft.AspNetCore.Mvc;
// using _123.Data;
// using _123.Models;
// using System.Linq;
// using System.Threading.Tasks;

// namespace _123.Controllers
// {
//     public class AccountController : Controller
//     {
//         private readonly ApplicationDbContext _context;

//         public AccountController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         // GET: /Account/SignUp
//         public IActionResult SignUp()
//         {
//             return View();
//         }

//         // POST: /Account/SignUp
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> SignUp(Users userAccount)
//         {
//             if (ModelState.IsValid)
//             {
//                 // Mã hóa mật khẩu trước khi lưu
//                 // userAccount.Password = BCrypt.Net.BCrypt.HashPassword(userAccount.Password);
//                 _context.Users.Add(userAccount);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction("Login");
//             }
//             return View(userAccount);
//         }

//         // GET: /Account/Login
//         public IActionResult Login()
//         {
//             return View();
//         }

//         // POST: /Account/Login
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Login(string username, string password)
//         {
//             var userAccount = _context.Users.SingleOrDefault(u => u.Username == username);
//             if (userAccount != null && BCrypt.Net.BCrypt.Verify(password, userAccount.Password))
//             {
//                 // Đăng nhập thành công
//                 return RedirectToAction("Index", "Home");
//             }
//             ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
//             return View();
//         }
//     }
// }

using Microsoft.AspNetCore.Mvc;
using _123.Models;
using System.Linq;
using System.Threading.Tasks;
using _123.Services;

namespace _123.Controllers
{
    public class AccountController : Controller
    {
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

        // // GET: /Account/Login
        // public IActionResult Login()
        // {
        //     return View();
        // }

        // POST: /Account/Login
        [HttpPost]
        // [ValidateAntiForgeryToken]
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