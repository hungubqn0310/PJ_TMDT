using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/category")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        // Trang danh sách Categories
        public IActionResult Index()
        {
            var categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = CategoryService.GetCategories();
            // Gửi dữ liệu đến View
            return View("Views/Admin/categories.cshtml", categoryViewModel);
        }

        // Hiển thị giao diện thêm Category
        [HttpGet("add")]
        public IActionResult Add()
        {
            Category category = new Category();
            return PartialView("/Views/Admin/categoryadd.cshtml", category);
        }

        // Xử lý thêm Category
        [HttpPost("add")]
        public IActionResult Add(Category category)
        {
            CategoryService.CreateCategory(category);
            return new RedirectResult("/admin/category");
        }

        // Hiển thị giao diện sửa Category
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            Category category = CategoryService.GetCategoryById(id);
            return PartialView("/Views/Admin/categoryedit.cshtml", category);
        }

        // Xử lý sửa Category
        [HttpPost("edit")]
        public IActionResult Edit(Category category)
        {
            CategoryService.UpdateCategory(category);
            return new RedirectResult("/admin/category");
        }

        // Hiển thị giao diện xóa Category
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Category category = CategoryService.GetCategoryById(id);
            return PartialView("/Views/Admin/categorydelete.cshtml", category);
        }

        // Xử lý xóa Category
        [HttpPost("delete")]
        public IActionResult Delete(Category category)
        {
            CategoryService.DeleteCategory(category.CategoryId);
            return new RedirectResult("/admin/category");
        }

        // Xử lý lỗi
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
