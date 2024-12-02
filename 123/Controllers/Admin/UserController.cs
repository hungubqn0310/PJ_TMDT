using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;

namespace _123.Controllers
{
    [Route("admin/user")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            string query = "SELECT * FROM users";
            DataTable users1 = DatabaseHelper.ExecuteQuery(query);
            
            var users = new List<string> { "User1", "User2", "User3" };
            // Gửi dữ liệu đến View
            return View("Views/Admin/users.cshtml",users);
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}