using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/productsupplier")]
    public class ProductSupplierController : Controller
    {
        private readonly ILogger<ProductSupplierController> _logger;

        public ProductSupplierController(ILogger<ProductSupplierController> logger)
        {
            _logger = logger;
        }

        // Hiển thị danh sách nhà cung cấp sản phẩm
        public IActionResult Index()
        {
            var productSupplierViewModel = new ProductSupplierViewModel();
            productSupplierViewModel.ProductSuppliers = ProductSupplierService.GetProductSuppliers();
            // Gửi dữ liệu đến View
            return View("Views/Admin/productsuppliers.cshtml", productSupplierViewModel);
        }

        // Trang thêm nhà cung cấp sản phẩm mới (GET)
        [HttpGet("add")]
        public IActionResult Add()
        {
            ProductSupplier productSupplier = new ProductSupplier();
            return PartialView("/Views/Admin/productsupplieradd.cshtml", productSupplier);
        }

        // Xử lý thêm nhà cung cấp sản phẩm mới (POST)
        [HttpPost("add")]
        public IActionResult Add(ProductSupplier productSupplier)
        {
            ProductSupplierService.CreateProductSupplier(productSupplier);
            return new RedirectResult("/admin/product-supplier");
        }

        // Trang sửa nhà cung cấp sản phẩm (GET)
        [HttpGet("edit")]
        public IActionResult Edit(string pd, int id)
        {
            ProductSupplier productSupplier = ProductSupplierService.GetProductSupplierById(pd,id);
            return PartialView("/Views/Admin/productsupplieredit.cshtml", productSupplier);
        }

        // Xử lý sửa nhà cung cấp sản phẩm (POST)
        [HttpPost("edit")]
        public IActionResult Edit(ProductSupplier productSupplier)
        {
            ProductSupplierService.UpdateProductSupplier(productSupplier);
            return new RedirectResult("/admin/product-supplier");
        }

        // Trang xóa nhà cung cấp sản phẩm (GET)
        [HttpGet("delete")]
        public IActionResult Delete(string pd,int id)
        {
            ProductSupplier productSupplier = ProductSupplierService.GetProductSupplierById(pd,id);
            return PartialView("/Views/Admin/productsupplierdelete.cshtml", productSupplier);
        }

        // Xử lý xóa nhà cung cấp sản phẩm (POST)
        [HttpPost("delete")]
        public IActionResult Delete(ProductSupplier productSupplier)
        {
            Console.WriteLine(productSupplier.SupplierId);
            ProductSupplierService.DeleteProductSupplier(productSupplier.SupplierId);
            return new RedirectResult("/admin/product-supplier");
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
