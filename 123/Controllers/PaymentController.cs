using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using _123.Services.Momo;
using _123.Models;

namespace _123.Controllers
{
    public class PaymentController : Controller
    {
        private IMomoService _momoService;
        // private readonly IVnPayService _vnPayService;
        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;
            
        }
        [HttpPost]
        
        public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model)
        {
            var response = await _momoService.CreatePaymentMomo(model);
            return Redirect(response.PayUrl);
        }

    }
}
