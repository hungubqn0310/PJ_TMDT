using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Services;
using System.Collections.Generic;
using System.Linq;

namespace _123.Controllers
{
    [Route("admin/productdiscount")]
    public class ProductDiscountController : Controller
    {
        private readonly ILogger<ProductDiscountController> _logger;

        public ProductDiscountController(ILogger<ProductDiscountController> logger)
        {
            _logger = logger;
        }

        // Hiển thị danh sách giảm giá sản phẩm
        public IActionResult Index()
        {
            // Lấy danh sách ProductDiscount từ dịch vụ
            var productDiscounts = ProductDiscountService.GetProductDiscounts();

            // Lấy danh sách sản phẩm và giảm giá
            var products = ProductService.GetProducts() ?? new List<Product>();
            var discounts = DiscountService.GetDiscounts() ?? new List<Discount>();

            // Ánh xạ ProductName và DiscountName
            foreach (var pd in productDiscounts)
            {
                var product = products.FirstOrDefault(p => p.ProductId == pd.ProductId);
                var discount = discounts.FirstOrDefault(d => d.DiscountId == pd.DiscountId);

                pd.Product = product; // Gắn thông tin sản phẩm
                pd.Discount = discount; // Gắn thông tin giảm giá
            }

            // Chuẩn bị ViewModel
            var productDiscountViewModel = new ProductDiscountViewModel
            {
                ProductDiscounts = productDiscounts
            };

            // Truyền danh sách sản phẩm và giảm giá vào ViewBag
            ViewBag.Products = products;
            ViewBag.Discounts = discounts;

            return View("Views/Admin/productdiscounts.cshtml", productDiscountViewModel);
        }

        // Trang thêm giảm giá sản phẩm mới (GET)
        [HttpGet("add")]
        public IActionResult Add()
        {
            var productDiscount = new ProductDiscount();
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();
            ViewBag.Discounts = DiscountService.GetDiscounts() ?? new List<Discount>();

            return PartialView("/Views/Admin/productdiscountadd.cshtml", productDiscount);
        }

        // Xử lý thêm giảm giá sản phẩm mới (POST)
        [HttpPost("add")]
        public IActionResult Add(ProductDiscount productDiscount)
        {
            try
            {
                ProductDiscountService.CreateProductDiscount(productDiscount);
                return new RedirectResult("/admin/productdiscount");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm giảm giá sản phẩm.");
                return RedirectToAction("Error");
            }
        }

        // Trang sửa giảm giá sản phẩm (GET)
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var productDiscount = ProductDiscountService.GetDiscountsByProductId(id);
            if (productDiscount == null)
            {
                return RedirectToAction("Error");
            }

            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();
            ViewBag.Discounts = DiscountService.GetDiscounts() ?? new List<Discount>();

            return PartialView("/Views/Admin/productdiscountedit.cshtml", productDiscount);
        }

        // Xử lý sửa giảm giá sản phẩm (POST)
        [HttpPost("edit")]
        public IActionResult Edit(ProductDiscount productDiscount)
        {
            try
            {
                ProductDiscountService.UpdateProductDiscount(productDiscount);
                return new RedirectResult("/admin/productdiscount");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi sửa giảm giá sản phẩm.");
                return RedirectToAction("Error");
            }
        }

        // Trang xóa giảm giá sản phẩm (GET)
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var productDiscount = ProductDiscountService.GetDiscountsByProductId(id);
            if (productDiscount == null)
            {
                return RedirectToAction("Error");
            }

            return PartialView("/Views/Admin/productdiscountdelete.cshtml", productDiscount);
        }

        // Xử lý xóa giảm giá sản phẩm (POST)
        [HttpPost("delete")]
        public IActionResult DeleteConfirmed(int pdId)
        {
            try
            {
                ProductDiscountService.DeleteProductDiscount(pdId);
                return new RedirectResult("/admin/productdiscount");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa giảm giá sản phẩm.");
                return RedirectToAction("Error");
            }
        }

        // Trang lỗi (Error)
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
