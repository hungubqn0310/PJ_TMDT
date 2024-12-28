using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using _123.Services.Momo;
using _123.Models.Momo;
using _123.Models.OrderMomo;

namespace _123.Controllers
{
    
    public class PaymentController : Controller
    {
        private IMomoService _momoService;

public PaymentController(IMomoService momoService)
    {
        _momoService = momoService;
    }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model)
        {
            var response = await _momoService.CreatePaymentAsync(model);
            return Redirect(response.PayUrl);
        }

        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(response);
        }
}}
