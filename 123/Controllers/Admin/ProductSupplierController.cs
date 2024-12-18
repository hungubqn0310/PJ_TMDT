using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Services;
using System.Collections.Generic;
using System;

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
    // Lấy danh sách ProductSupplier từ dịch vụ
            var productSuppliers = ProductSupplierService.GetProductSuppliers();

            // Lấy danh sách sản phẩm và nhà cung cấp
            var products = ProductService.GetProducts() ?? new List<Product>();
            var suppliers = SupplierService.GetSuppliers() ?? new List<Supplier>();

            // Ánh xạ ProductName và SupplierName
            foreach (var ps in productSuppliers)
            {
                var product = products.FirstOrDefault(p => p.ProductId == ps.ProductId);
                var supplier = suppliers.FirstOrDefault(s => s.SupplierId == ps.SupplierId);

                ps.Product = product; // Gắn thông tin sản phẩm
                ps.Supplier = supplier; // Gắn thông tin nhà cung cấp
            }

            // Chuẩn bị ViewModel
            var productSupplierViewModel = new ProductSupplierViewModel
            {
                ProductSuppliers = productSuppliers
            };

            // Truyền danh sách sản phẩm và nhà cung cấp vào ViewBag
            ViewBag.Products = products;
            ViewBag.Suppliers = suppliers;

            return View("Views/Admin/productsuppliers.cshtml", productSupplierViewModel);
        }

        // Trang thêm nhà cung cấp sản phẩm mới (GET)
        [HttpGet("add")]
        public IActionResult Add()
        {
            var productSupplier = new ProductSupplier();
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();
            ViewBag.Suppliers = SupplierService.GetSuppliers() ?? new List<Supplier>();

            return PartialView("/Views/Admin/productsupplieradd.cshtml", productSupplier);
        }

        // Xử lý thêm nhà cung cấp sản phẩm mới (POST)
        [HttpPost("add")]
        public IActionResult Add(ProductSupplier productSupplier)
        {
            try
            {
                ProductSupplierService.CreateProductSupplier(productSupplier);
                return new RedirectResult("/admin/productsupplier");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm nhà cung cấp sản phẩm.");
                return RedirectToAction("Error");
            }
        }

        // Trang sửa nhà cung cấp sản phẩm (GET)
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var productSupplier = ProductSupplierService.GetProductSupplierById(id);
            if (productSupplier == null)
            {
                return RedirectToAction("Error");
            }

            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();
            ViewBag.Suppliers = SupplierService.GetSuppliers() ?? new List<Supplier>();

            return PartialView("/Views/Admin/productsupplieredit.cshtml", productSupplier);
        }
        // Xử lý sửa nhà cung cấp sản phẩm (POST)
        [HttpPost("edit")]
        public IActionResult Edit(ProductSupplier productSupplier)
        {
            try
            {
                ProductSupplierService.UpdateProductSupplier(productSupplier);
                return new RedirectResult("/admin/productsupplier");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi sửa nhà cung cấp sản phẩm.");
                return RedirectToAction("Error");
            }
        }

        // Trang xóa nhà cung cấp sản phẩm (GET)
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var productSupplier = ProductSupplierService.GetProductSupplierById(id);
            if (productSupplier == null)
            {
                return RedirectToAction("Error");
            }

            return PartialView("/Views/Admin/productsupplierdelete.cshtml", productSupplier);
        }

        // Xử lý xóa nhà cung cấp sản phẩm (POST)
        [HttpPost("delete")]
        public IActionResult DeleteConfirmed(int psId)
        {
            try
            {
                ProductSupplierService.DeleteProductSupplier(psId);
                return new RedirectResult("/admin/productsupplier");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa nhà cung cấp sản phẩm.");
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
