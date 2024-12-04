using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/material")]
    public class MaterialController : Controller
    {
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(ILogger<MaterialController> logger)
        {
            _logger = logger;
        }

        // Trang danh sách Materials
        public IActionResult Index()
        {
            var materialViewModel = new MaterialViewModel
            {
                Materials = MaterialService.GetMaterials()
            };
            return View("Views/Admin/materials.cshtml", materialViewModel);
        }

        // Hiển thị giao diện thêm Material
        [HttpGet("add")]
        public IActionResult Add()
        {
            Material material = new Material();
            return PartialView("/Views/Admin/materialadd.cshtml", material);
        }

        // Xử lý thêm Material
        [HttpPost("add")]
        public IActionResult Add(Material material)
        {
            MaterialService.CreateMaterial(material);
            return new RedirectResult("/admin/material");
        }

        // Hiển thị giao diện sửa Material
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            Material material = MaterialService.GetMaterialById(id);
            return PartialView("/Views/Admin/materialedit.cshtml", material);
        }

        // Xử lý sửa Material
        [HttpPost("edit")]
        public IActionResult Edit(Material material)
        {
            MaterialService.UpdateMaterial(material);
            return new RedirectResult("/admin/material");
        }

        // Hiển thị giao diện xóa Material
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Material material = MaterialService.GetMaterialById(id);
            return PartialView("/Views/Admin/materialdelete.cshtml", material);
        }

        // Xử lý xóa Material
        [HttpPost("delete")]
        public IActionResult Delete(Material material)
        {
            MaterialService.DeleteMaterial(material.MaterialId);
            return new RedirectResult("/admin/material");
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
