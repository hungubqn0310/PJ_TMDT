using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Products = ProductService.GetProducts();
            ViewBag.Materials = MaterialService.GetMaterials() ?? new List<Material>();
            // Gửi dữ liệu đến View
            ViewBag.Categories = CategoryService.GetCategories() ?? new List<Category>();
            return View("Views/Admin/products.cshtml", productViewModel);
        }

        [HttpGet("add")]
    public IActionResult Add()
    {
        Product product = new Product();

        // Lấy danh sách thể loại và chất liệu
        ViewBag.Categories = CategoryService.GetCategories() ?? new List<Category>();  // Giả sử phương thức GetCategories() lấy danh sách thể loại
        ViewBag.Materials = MaterialService.GetMaterials() ?? new List<Material>();  // Giả sử phương thức GetMaterials() lấy danh sách chất liệu

        return PartialView("/Views/Admin/productadd.cshtml", product);
    }

    [HttpPost("add")]
    public IActionResult Add(Product product)
    {
        ProductService.CreateProduct(product);
        return new RedirectResult("/admin/product");
    }

    [HttpGet("edit")]
    public IActionResult Edit(string id)
    {
        Product product = ProductService.GetProductById(id);

        // Lấy danh sách thể loại và chất liệu khi chỉnh sửa
        ViewBag.Categories = CategoryService.GetCategories();
        ViewBag.Materials = MaterialService.GetMaterials();

        return PartialView("/Views/Admin/productedit.cshtml", product);
    }

    [HttpPost("edit")]
    public IActionResult Edit(Product product)
    {
        ProductService.UpdateProduct(product);
        return new RedirectResult("/admin/product");
    }

    [HttpGet("delete")]
    public IActionResult Delete(string id)
    {
        Product product = ProductService.GetProductById(id);
        return PartialView("/Views/Admin/productdelete.cshtml", product);
    }

    [HttpPost("delete")]
    public IActionResult Delete(Product product)
    {
        ProductService.DeleteProduct(product.ProductId);
        return new RedirectResult("/admin/product");
    }

    [HttpGet("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    }
}
