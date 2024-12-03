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

<<<<<<< HEAD
        // API Key (nên được lấy từ file cấu hình hoặc biến môi trường)
=======
        // API Key của bạn
>>>>>>> 879ad2868271f002578fbc32d7c1081a53e36206
        private const string ApiKey = "goldapi-hqv9xsm43maetf-io";

        [HttpGet]
        public async Task<IActionResult> GetGoldPrice()
        {
            try
            {
                var url = "https://www.goldapi.io/api/XAU/USD"; // API URL
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
<<<<<<< HEAD
                requestMessage.Headers.Add("x-access-token", ApiKey); // Thêm API Key vào Header
=======
                requestMessage.Headers.Add("x-access-token", ApiKey);
>>>>>>> 879ad2868271f002578fbc32d7c1081a53e36206

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
<<<<<<< HEAD
                var url = "https://www.goldapi.io/api/XAU/USD"; // API URL
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Add("x-access-token", ApiKey); // Thêm API Key vào Header

                // Gửi yêu cầu đến API
                var response = await _httpClient.SendAsync(requestMessage);
=======
                // Lấy giá vàng
                var goldUrl = "https://www.goldapi.io/api/XAU/USD";
                var goldRequest = new HttpRequestMessage(HttpMethod.Get, goldUrl);
                goldRequest.Headers.Add("x-access-token", ApiKey);
                var goldResponse = await _httpClient.SendAsync(goldRequest);

                // Lấy giá bạc
                var silverUrl = "https://www.goldapi.io/api/XAG/USD";
                var silverRequest = new HttpRequestMessage(HttpMethod.Get, silverUrl);
                silverRequest.Headers.Add("x-access-token", ApiKey);
                var silverResponse = await _httpClient.SendAsync(silverRequest);
>>>>>>> 879ad2868271f002578fbc32d7c1081a53e36206

                if (goldResponse.IsSuccessStatusCode && silverResponse.IsSuccessStatusCode)
                {
                    // Xử lý dữ liệu giá vàng
                    var goldData = await goldResponse.Content.ReadAsStringAsync();
                    var goldPriceData = JsonConvert.DeserializeObject<GoldPriceResponse>(goldData);

<<<<<<< HEAD
                    if (goldPriceData == null || goldPriceData.Price <= 0)
                    {
                        return StatusCode(500, "Dữ liệu từ API không hợp lệ.");
                    }

                    // Trả về dữ liệu giá vàng cho biểu đồ
                    return Json(new { price = goldPriceData.Price, timestamp = DateTime.UtcNow });
=======
                    // Xử lý dữ liệu giá bạc
                    var silverData = await silverResponse.Content.ReadAsStringAsync();
                    var silverPriceData = JsonConvert.DeserializeObject<GoldPriceResponse>(silverData);

                    // Trả về dữ liệu giá vàng và bạc
                    return Json(new
                    {
                        goldPrice = goldPriceData.Price,
                        silverPrice = silverPriceData.Price,
                        timestamp = DateTime.UtcNow
                    });
>>>>>>> 879ad2868271f002578fbc32d7c1081a53e36206
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
<<<<<<< HEAD
                return StatusCode(500, "Lỗi không xác định: " + ex.Message);
=======
                return StatusCode(500, "Lỗi khi lấy dữ liệu giá vàng và bạc: " + ex.Message);
>>>>>>> 879ad2868271f002578fbc32d7c1081a53e36206
            }
        }
    }

    // Định nghĩa lớp phản hồi từ API
    public class GoldPriceResponse
    {
<<<<<<< HEAD
        public string Symbol { get; set; } = string.Empty; // Ký hiệu của vàng (khởi tạo giá trị mặc định)
        public decimal Price { get; set; } // Giá vàng hiện tại
=======
        public string Symbol { get; set; }  // Ký hiệu (XAU hoặc XAG)
        public decimal Price { get; set; }  // Giá hiện tại
>>>>>>> 879ad2868271f002578fbc32d7c1081a53e36206
    }
}
