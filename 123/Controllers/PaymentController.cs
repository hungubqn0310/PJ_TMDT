using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _123.Services.Momo;
using _123.Models.Momo;
using _123.Models.OrderMomo;
using _123.Services.VNPay;
using _123.Services;
using _123.Models.VNPay;

namespace _123.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IMomoService _momoService;
        private readonly IVnPayService _vnPayService;
        private readonly ZaloPayService _zaloPayService;

        public PaymentController(IMomoService momoService, IVnPayService vnPayService, ZaloPayService zaloPayService)
        {
            _momoService = momoService;
            _vnPayService = vnPayService;
            _zaloPayService = zaloPayService;
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
            return View(response);
        }

        public class ZaloPaymentRequest
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        // [HttpPost]
        // public async Task<IActionResult> CreateZaloPaymentUrl(ZaloPaymentRequest model)
        // {
        //     var response = await ZaloPayService.CreatePaymentRequestAsync(model.Amount, model.Description);
        //     return Redirect(response.order_url);
        // }

    }
}