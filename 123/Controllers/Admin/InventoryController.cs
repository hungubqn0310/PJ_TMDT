using _123.Models;
using Microsoft.AspNetCore.Mvc;
using _123.Services;
using System.Diagnostics;

namespace _123.Controllers
{
    [Route("admin/inventory")]
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        // Hiển thị danh sách tồn kho
        public IActionResult Index()
        {
            var inventoryViewModel = new InventoryViewModel();
            inventoryViewModel.Inventories = InventoryService.GetInventories();
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();;
            // Gửi dữ liệu đến View
            return View("Views/Admin/inventories.cshtml", inventoryViewModel);
        }

        // Trang thêm mới tồn kho (GET)
        [HttpGet("add")]      
        public IActionResult Add() 
        {
            Inventory inventory = new Inventory(); // Tạo đối tượng Inventory rỗng
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();
            return PartialView("/Views/Admin/inventoryadd.cshtml", inventory);
        }

        // Thêm mới tồn kho (POST)
        [HttpPost("add")]
        public IActionResult Add(Inventory inventory)
        {
            InventoryService.CreateInventory(inventory);
            return new RedirectResult("/admin/inventory");
        }

        // Trang sửa thông tin tồn kho (GET)
        [HttpGet("edit")]      
        public IActionResult Edit(int id) 
        {
            Inventory inventory = InventoryService.GetInventoryById(id);
            return PartialView("/Views/Admin/inventoryedit.cshtml", inventory);
        }

        // Cập nhật thông tin tồn kho (POST)
        [HttpPost("edit")]
        public IActionResult Edit(Inventory inventory)
        {
            InventoryService.UpdateInventory(inventory);
            return new RedirectResult("/admin/inventory");
        }

        // Trang xác nhận xóa tồn kho (GET)
        [HttpGet("delete")]      
        public IActionResult Delete(int id) 
        {
            Inventory inventory = InventoryService.GetInventoryById(id);
            return PartialView("/Views/Admin/inventorydelete.cshtml", inventory);
        }

        // Xóa tồn kho (POST)
        [HttpPost("delete")]
        public IActionResult Delete(Inventory inventory)
        {
            InventoryService.DeleteInventory(inventory.InventoryId);
            return new RedirectResult("/admin/inventory");
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
