using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/supplier")]
    public class SupplierController : Controller
    {
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ILogger<SupplierController> logger)
        {
            _logger = logger;
        }

        // Hiển thị danh sách nhà cung cấp
        public IActionResult Index()
        {
            var supplierViewModel = new SupplierViewModel();
            supplierViewModel.Suppliers = SupplierService.GetSuppliers();
            // Gửi dữ liệu đến View
            return View("Views/Admin/suppliers.cshtml", supplierViewModel);
        }

        // Thêm nhà cung cấp (hiển thị form)
        [HttpGet("add")]      
        public IActionResult Add() 
        {
            Supplier supplier = new Supplier();
            return PartialView("/Views/Admin/supplieradd.cshtml", supplier);
        }

        // Xử lý thêm nhà cung cấp
        [HttpPost("add")]
        public IActionResult Add(Supplier supplier)
        {
            SupplierService.CreateSupplier(supplier);
            return new RedirectResult("/admin/supplier");
        }

        // Sửa nhà cung cấp (hiển thị form)
        [HttpGet("edit")]      
        public IActionResult Edit(int id) 
        {
            Supplier supplier = SupplierService.GetSupplierById(id);
            return PartialView("/Views/Admin/supplieredit.cshtml", supplier);
        }

        // Xử lý sửa nhà cung cấp
        [HttpPost("edit")]
        public IActionResult Edit(Supplier supplier)
        {
            SupplierService.UpdateSupplier(supplier);
            return new RedirectResult("/admin/supplier");
        }

        // Xóa nhà cung cấp (hiển thị form)
        [HttpGet("delete")]      
        public IActionResult Delete(int id) 
        {
            Supplier supplier = SupplierService.GetSupplierById(id);
            return PartialView("/Views/Admin/supplierdelete.cshtml", supplier);
        }

        // Xử lý xóa nhà cung cấp
        [HttpPost("delete")]
        public IActionResult Delete(Supplier supplier)
        {
            SupplierService.DeleteSupplier(supplier.SupplierId);
            return new RedirectResult("/admin/supplier");
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
