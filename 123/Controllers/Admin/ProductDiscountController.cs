using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using _123.Services;

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
            var productDiscountViewModel = new ProductDiscountViewModel();
            productDiscountViewModel.ProductDiscounts = ProductDiscountService.GetProductDiscounts();
            // Gửi dữ liệu đến View
            return View("Views/Admin/productdiscounts.cshtml", productDiscountViewModel);
        }

        // Hiển thị form thêm sản phẩm giảm giá
        [HttpGet("add")]      
        public IActionResult Add() 
        {
            ProductDiscount productDiscount = new ProductDiscount();
            return PartialView("/Views/Admin/productdiscountadd.cshtml", productDiscount);
        }

        // Xử lý thêm sản phẩm giảm giá
        [HttpPost("add")]
        public IActionResult Add(ProductDiscount productDiscount)
        {
            if (ModelState.IsValid)
            {
                ProductDiscountService.CreateProductDiscount(productDiscount.ProductId, productDiscount.DiscountId);
                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách giảm giá sản phẩm
            }
            return View("/Views/Admin/productdiscountadd.cshtml", productDiscount); // Nếu dữ liệu không hợp lệ, giữ lại form
        }

        // Hiển thị form chỉnh sửa sản phẩm giảm giá
        [HttpGet("edit/{id}")]      
        public IActionResult Edit(string id) 
        {
            ProductDiscount productDiscount = ProductDiscountService.GetDiscountsByProductId(id);
            return PartialView("/Views/Admin/productdiscountedit.cshtml", productDiscount);
        }

        // Xử lý chỉnh sửa sản phẩm giảm giá
        [HttpPost("edit")]
        public IActionResult Edit(ProductDiscount productDiscount)
        {
            if (ModelState.IsValid)
            {
                ProductDiscountService.UpdateProductDiscount(productDiscount);
                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách giảm giá sản phẩm
            }
            return View("/Views/Admin/productdiscountedit.cshtml", productDiscount); // Nếu dữ liệu không hợp lệ, giữ lại form
        }

        // Hiển thị form xóa sản phẩm giảm giá
        [HttpGet("delete/{id}")]      
        public IActionResult Delete(string id) 
        {
            var productDiscount = ProductDiscountService.GetDiscountsByProductId(id);

            return PartialView("/Views/Admin/productdiscountdelete.cshtml", productDiscount);
        }

        // Xử lý xóa sản phẩm giảm giá
        [HttpPost("delete")]
        public IActionResult Delete(ProductDiscount productDiscount)
        {
            ProductDiscountService.DeleteProductDiscount(productDiscount.ProductId, productDiscount.DiscountId);
            return RedirectToAction("Index"); // Chuyển hướng về trang danh sách giảm giá sản phẩm
        }

        // Hiển thị lỗi (nếu có)
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
