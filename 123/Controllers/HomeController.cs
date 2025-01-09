﻿using _123.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _123.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Category(string category)
        {
            switch (category.ToLower())
            {
                case "nhancauhon":
                    ViewData["Title"] = "Nhẫn cầu hôn";
                    break;
                case "nhancuoi":
                    ViewData["Title"] = "Nhẫn cưới";
                    break;
                case "trangsuc":
                    ViewData["Title"] = "Trang sức";
                    break;
                case "kimcuong":
                    ViewData["Title"] = "Kim cương";
                    break;
                case "mens":
                    ViewData["Title"] = "Men's";
                    break;
                default:
                    return NotFound(); // Nếu không khớp, trả về lỗi 404
            }

            return View("Category"); // Sử dụng chung một View
        }

        public IActionResult ThongTin(string topic)
        {
            switch (topic?.ToLower())
            {
                case "chinhsachthuhoivadoitra":
                    return View("ChinhSachThuHoiVaDoiTra");
                case "huongdandosize":
                    return View("HuongDanDoSize");
                case "chinhsachthanhtoan":
                    return View("ChinhSachThanhToan");
                case "chinhsachbaomatthongtin":
                    return View("ChinhSachBaoMatThongTin");
                case "giavang":
                    return View("GiaVang");
                default:
                    return NotFound(); // Nếu không tìm thấy topic, trả về lỗi 404
            }
        }
        
        public IActionResult KhuyenMai()
        {
            return View(); // Trả về view "KhuyenMai"
        }


        public IActionResult CauChuyenHanDK()
        {
            return View(); // Trả về view "CauChuyenHanDK"
        }

        public IActionResult CuaHangHanDK()
        {
            return View(); // Trả về view "CuaHangHanDK"
        }

        public IActionResult ProductDetail()
    {
        return View();
    }

        public IActionResult orderPayment()
    {
        return View();
    }

        public IActionResult paymentSuccessful()
    {
        return View();
    }

    public IActionResult cart()
    {
        return View();
    }

public IActionResult transactionHistory()
    {
        return View();
    }

    public IActionResult YeuThich()
    {
        return View();
    }

    public IActionResult UserInfo()
    {
        return View();
    }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}