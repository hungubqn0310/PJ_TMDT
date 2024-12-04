using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace _123.Controllers
{
    public class GoldPriceController : Controller
    {
        private readonly HttpClient _httpClient;

        public GoldPriceController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // API Key của bạn
        private const string ApiKey = "goldapi-4af9019m49hnpo9-io";

        [HttpGet]
        public async Task<IActionResult> GetGoldPrice()
        {
            try
            {
                var url = "https://www.goldapi.io/api/XAU/USD"; // Lấy giá vàng (XAU) theo USD
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Add("x-access-token", ApiKey);

                var response = await _httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var goldPriceData = JsonConvert.DeserializeObject<GoldPriceResponse>(responseData);

                    return Json(goldPriceData);
                }
                else
                {
                    return StatusCode(500, "Không thể lấy dữ liệu từ API.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi lấy dữ liệu giá vàng: " + ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi lấy dữ liệu giá vàng và bạc: " + ex.Message);
            }
        }
    }

    public class GoldPriceResponse
    {
        public string Symbol { get; set; }  // Ký hiệu (XAU hoặc XAG)
        public decimal Price { get; set; }  // Giá hiện tại
    }
}
