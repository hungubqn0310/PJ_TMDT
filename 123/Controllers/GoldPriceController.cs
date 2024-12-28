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
        private const string ApiKey = "goldapi-2ks8qsm4gpvhxp-io";

        [HttpGet]
        public async Task<IActionResult> GetGoldPrice()
        {
            try
            {
                var url = "https://www.goldapi.io/api/XAU/USD"; // API URL
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Add("x-access-token", ApiKey);

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

                if (goldResponse.IsSuccessStatusCode && silverResponse.IsSuccessStatusCode)
                {
                    // Xử lý dữ liệu giá vàng
                    var goldData = await goldResponse.Content.ReadAsStringAsync();
                    var goldPriceData = JsonConvert.DeserializeObject<GoldPriceResponse>(goldData);

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
                }
                else
                {
                    return StatusCode(500, "Không thể lấy dữ liệu từ API.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Lỗi kết nối đến API: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi lấy dữ liệu giá vàng và bạc: " + ex.Message);
            }
        }
    }

    // Định nghĩa lớp phản hồi từ API
    public class GoldPriceResponse
    {
        public string Symbol { get; set; }  // Ký hiệu (XAU hoặc XAG)
        public decimal Price { get; set; }  // Giá hiện tại
    }
}