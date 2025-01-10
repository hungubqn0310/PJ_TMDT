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
        private readonly EmailSender _emailSender;

        public AccountController(UserManager<IdentityUser> userManager, EmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
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
                var st =  AuthService.Signup(userAccount); // Đảm bảo rằng AuthService.Signup là async nếu cần

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

        // GET: /Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // Xử lý yêu cầu quên mật khẩu
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Không tiết lộ nếu email không tồn tại
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            // Tạo mã đặt lại mật khẩu
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { code }, protocol: HttpContext.Request.Scheme);

            // Gửi email
            await _emailSender.SendEmailAsync(email, "Đặt lại mật khẩu", 
                $"Bạn đã yêu cầu đặt lại mật khẩu. Vui lòng <a href='{callbackUrl}'>nhấp vào đây</a> để đặt lại mật khẩu.");

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        // Xác nhận rằng email đã được gửi
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string code, string password, string email)
        {
            var user = await _userManager.FindByEmailAsync(email); // Cần phải lấy email từ mã hoặc từ một nguồn khác
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, code, password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }
            return View();
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}