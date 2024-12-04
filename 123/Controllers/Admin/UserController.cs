using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _123.Helpers;
using System.Data;
using _123.Services;

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
            var userViewModel = new UserViewModel();
            userViewModel.Users = UserService.GetUsers();
            // Gửi dữ liệu đến View
            return View("Views/Admin/users.cshtml",userViewModel);
        }

        [HttpGet("add")]      
        public IActionResult Add() 
        {
            User user = new User();
            return PartialView("/Views/Admin/useradd.cshtml", user);
        }

        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            UserService.CreateUser(user);
            return new RedirectResult("/admin/user");
        }

        [HttpGet("edit")]      
        public IActionResult Edit(int id) 
        {
            User user = UserService.GetUserById(id);
            return PartialView("/Views/Admin/useredit.cshtml", user);
        }

        [HttpPost("edit")]
        public IActionResult Edit(User user)
        {
            UserService.UpdateUser(user);
            return new RedirectResult("/admin/user");
        }

        [HttpGet("delete")]      
        public IActionResult Delete(int id) 
        {
            User user = UserService.GetUserById(id);
            return PartialView("/Views/Admin/userdelete.cshtml", user);
        }

        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {
            Console.WriteLine(user.user_id);
            UserService.DeleteUser(user.user_id);
            return new RedirectResult("/admin/user");
        }

        
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}