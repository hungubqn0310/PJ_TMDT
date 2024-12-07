using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _123.Controllers
{
    public class GoldPriceController : Controller
    {
        private readonly HttpClient _httpClient;

        public GoldPriceController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // API Key của bạn
        private const string ApiKey = "goldapi-4af9019m49hnpo9-io";
       

        [HttpGet]
        public async Task<IActionResult> GetGoldPrice()
        {
            try
            {
                var url = "https://www.goldapi.io/api/XAU/USD"; // API URL
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Add("x-access-token", ApiKey); // Thêm API Key vào Header

                // Gửi yêu cầu đến API
                var response = await _httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var goldPriceData = JsonConvert.DeserializeObject<GoldPriceResponse>(responseData);

                    if (goldPriceData == null || goldPriceData.Price <= 0)
                    {
                        return StatusCode(500, "Dữ liệu từ API không hợp lệ.");
                    }

                    return Json(goldPriceData); // Trả về JSON
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Không thể lấy dữ liệu từ API.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Lỗi kết nối đến API: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi không xác định: " + ex.Message);
            }
        }

        // Gửi dữ liệu giá vàng và bạc để vẽ biểu đồ
        [HttpGet]
public async Task<IActionResult> GetGoldPriceForChart()
{
    try
    {
        var url = "https://www.goldapi.io/api/XAU/USD"; // API URL
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        requestMessage.Headers.Add("x-access-token", ApiKey); // Thêm API Key vào Header

        // Gửi yêu cầu đến API
        var response = await _httpClient.SendAsync(requestMessage);

        if (response.IsSuccessStatusCode)
        {
            // Xử lý dữ liệu giá vàng
            var goldData = await response.Content.ReadAsStringAsync();
            var goldPriceData = JsonConvert.DeserializeObject<GoldPriceResponse>(goldData);

            if (goldPriceData == null || goldPriceData.Price <= 0)
            {
                return StatusCode(500, "Dữ liệu từ API không hợp lệ.");
            }

            // Trả về dữ liệu giá vàng cho biểu đồ
            return Json(new { price = goldPriceData.Price, timestamp = DateTime.UtcNow });
        }
        else
        {
            return StatusCode((int)response.StatusCode, "Không thể lấy dữ liệu từ API.");
        }
    }
    catch (HttpRequestException ex)
    {
        return StatusCode(500, "Lỗi kết nối đến API: " + ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Lỗi không xác định: " + ex.Message);
    }
}


    // Định nghĩa lớp phản hồi từ API
    public class GoldPriceResponse
    {
        public string Symbol { get; set; } = string.Empty; // Ký hiệu của vàng (khởi tạo giá trị mặc định)
        public decimal Price { get; set; } // Giá vàng hiện tại
    }
}
}