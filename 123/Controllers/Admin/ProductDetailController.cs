using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/productdetail")]
    public class ProductDetailController : Controller
    {
        private readonly ILogger<ProductDetailController> _logger;

        public ProductDetailController(ILogger<ProductDetailController> logger)
        {
            _logger = logger;
        }

        // Hiển thị danh sách ProductDetail
        public IActionResult Index()
        {
            var productDetailViewModel = new ProductDetailViewModel();
            productDetailViewModel.ProductDetails = ProductDetailService.GetProductDetails();

            // Lấy danh sách sản phẩm để hiển thị tùy chọn
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();

            return View("Views/Admin/productdetails.cshtml", productDetailViewModel);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            ProductDetail productDetail = new ProductDetail();

            // Lấy danh sách sản phẩm
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();

            return PartialView("/Views/Admin/productdetailadd.cshtml", productDetail);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(ProductDetail productDetail)
        {
            ProductDetailService.CreateProductDetail(productDetail);
            return new RedirectResult("/admin/productdetail");
        }

        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            ProductDetail productDetail = ProductDetailService.GetProductDetailById(id);

            // Lấy danh sách sản phẩm
            ViewBag.Products = ProductService.GetProducts();

            return PartialView("/Views/Admin/productdetailedit.cshtml", productDetail);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(ProductDetail productDetail)
        {
            ProductDetailService.UpdateProductDetail(productDetail);
            return new RedirectResult("/admin/productdetail");
        }

        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            ProductDetail productDetail = ProductDetailService.GetProductDetailById(id);
            return PartialView("/Views/Admin/productdetaildelete.cshtml", productDetail);
        }

        [HttpPost("delete")]
        public IActionResult Delete(ProductDetail productDetail)
        {
            ProductDetailService.DeleteProductDetail(productDetail.ProductDetailId);
            return new RedirectResult("/admin/productdetail");
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
