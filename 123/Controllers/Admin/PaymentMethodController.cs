using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/payment-method")]
    public class PaymentMethodController : Controller
    {
        private readonly ILogger<PaymentMethodController> _logger;

        public PaymentMethodController(ILogger<PaymentMethodController> logger)
        {
            _logger = logger;
        }

        // Hiển thị danh sách các phương thức thanh toán
        public IActionResult Index()
        {
            var paymentMethodViewModel = new PaymentMethodViewModel();
            paymentMethodViewModel.PaymentMethods = PaymentMethodService.GetPaymentMethods();
            return View("Views/Admin/paymentmethod.cshtml", paymentMethodViewModel);
        }

        // Hiển thị form thêm phương thức thanh toán
        [HttpGet("add")]
        public IActionResult Add()
        {
            var paymentMethod = new PaymentMethod();
            return PartialView("/Views/Admin/paymentmethodadd.cshtml", paymentMethod);
        }

        // Thêm mới phương thức thanh toán
        [HttpPost("add")]
        public IActionResult Add(PaymentMethod paymentMethod)
        {
            PaymentMethodService.CreatePaymentMethod(paymentMethod);
            return new RedirectResult("/admin/payment-method");
        }

        // Hiển thị form sửa phương thức thanh toán
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var paymentMethod = PaymentMethodService.GetPaymentMethodById(id);
            return PartialView("/Views/Admin/paymentmethodedit.cshtml", paymentMethod);
        }

        // Cập nhật phương thức thanh toán
        [HttpPost("edit")]
        public IActionResult Edit(PaymentMethod paymentMethod)
        {
            PaymentMethodService.UpdatePaymentMethod(paymentMethod);
            return new RedirectResult("/admin/payment-method");
        }

        // Hiển thị form xóa phương thức thanh toán
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            var paymentMethod = PaymentMethodService.GetPaymentMethodById(id);
            return PartialView("/Views/Admin/paymentmethoddelete.cshtml", paymentMethod);
        }

        // Xóa phương thức thanh toán
        [HttpPost("delete")]
        public IActionResult Delete(PaymentMethod paymentMethod)
        {
            PaymentMethodService.DeletePaymentMethod(paymentMethod.PaymentMethodId);
            return new RedirectResult("/admin/payment-method");
        }

        // Trang lỗi (nếu có)
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
