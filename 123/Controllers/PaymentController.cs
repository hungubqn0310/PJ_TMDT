using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _123.Services.Momo;
using _123.Models.Momo;
using _123.Models.OrderMomo;
using _123.Services.VNPay;
using _123.Models.VNPay;

namespace _123.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IMomoService _momoService;
        private readonly IVnPayService _vnPayService;

        public PaymentController(IMomoService momoService, IVnPayService vnPayService)
        {
            _momoService = momoService;
            _vnPayService = vnPayService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateMomoPaymentUrl(OrderInfoModel model)
        {
            var response = await _momoService.CreatePaymentAsync(model);
            return Redirect(response.PayUrl);
        }

        [HttpPost]
        public IActionResult CreateVNPayPaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }

        [HttpGet]
        public IActionResult MomoPaymentCallback()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(response);
        }

        [HttpGet]
        public IActionResult VNPayPaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            return Json(response);
        }
    }
}