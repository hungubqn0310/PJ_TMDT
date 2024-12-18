using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

namespace _123.Controllers
{
    [Route("admin/role")]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var roleViewModel = new RoleViewModel();
            roleViewModel.Roles = RoleService.GetRoles();
            // Gửi dữ liệu đến View
            return View("Views/Admin/roles.cshtml", roleViewModel);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            Role role = new Role();
            return PartialView("/Views/Admin/roleadd.cshtml", role);
        }

        [HttpPost("add")]
        public IActionResult Add(Role role)
        {
            RoleService.CreateRole(role);
            return new RedirectResult("/admin/role");
        }

        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            Role role = RoleService.GetRoleById(id);
            return PartialView("/Views/Admin/roleedit.cshtml", role);
        }

        [HttpPost("edit")]
        public IActionResult Edit(Role role)
        {
            RoleService.UpdateRole(role);
            return new RedirectResult("/admin/role");
        }

        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Role role = RoleService.GetRoleById(id);
            return PartialView("/Views/Admin/roledelete.cshtml", role);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Role role)
        {
            Console.WriteLine(role.Id);
            RoleService.DeleteRole(role.Id);
            return new RedirectResult("/admin/role");
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
