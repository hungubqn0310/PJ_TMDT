using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/discount")]
    public class DiscountController : Controller
    {
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(ILogger<DiscountController> logger)
        {
            _logger = logger;
        }

        // Hiển thị danh sách giảm giá
        public IActionResult Index()
        {
            var discountViewModel = new DiscountViewModel
            {
                Discounts = DiscountService.GetDiscounts()
            };
            return View("Views/Admin/discounts.cshtml", discountViewModel);
        }

        // Hiển thị form thêm mới
        [HttpGet("add")]
        public IActionResult Add()
        {
            Discount discount = new Discount();
            return PartialView("/Views/Admin/discountadd.cshtml", discount);
        }

        // Xử lý thêm mới
        [HttpPost("add")]
        public IActionResult Add(Discount discount)
        {
            DiscountService.CreateDiscount(discount);
            return new RedirectResult("/admin/discount");
        }

        // Hiển thị form chỉnh sửa
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            Discount discount = DiscountService.GetDiscountById(id);
            return PartialView("/Views/Admin/discountedit.cshtml", discount);
        }

        // Xử lý chỉnh sửa
        [HttpPost("edit")]
        public IActionResult Edit(Discount discount)
        {
            DiscountService.UpdateDiscount(discount);
            return new RedirectResult("/admin/discount");
        }

        // Hiển thị form xóa
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Discount discount = DiscountService.GetDiscountById(id);
            return PartialView("/Views/Admin/discountdelete.cshtml", discount);
        }

        // Xử lý xóa
        [HttpPost("delete")]
        public IActionResult Delete(Discount discount)
        {
            DiscountService.DeleteDiscount(discount.DiscountId);
            return new RedirectResult("/admin/discount");
        }

        // Trang lỗi
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
