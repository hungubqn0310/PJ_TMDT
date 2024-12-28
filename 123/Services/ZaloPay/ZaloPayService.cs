using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace _123.Services
{
public class ZaloPayService
{
    private readonly string ZaloPayApiUrl;
    private readonly string AppId;
    private readonly string AppUser;
    private readonly string Key1;

    public ZaloPayService(IConfiguration configuration)
    {
        ZaloPayApiUrl = configuration["ZaloPay:PaymentUrl"];
        AppId = configuration["ZaloPay:AppId"];
        AppUser = configuration["ZaloPay:AppUser"];
        Key1 = configuration["ZaloPay:Key1"];
    }

    public async Task<CreateZalopayPayResponse> CreateZaloPayOrder(decimal amount)
    {
        try
        {
            var orderId = Guid.NewGuid().ToString(); // Unique Order ID
            var embedData = "{}";
            var items = "[]";

            // Tạo payload gửi đến ZaloPay
            var payload = new ZaloPayPayload
            {
                app_id = AppId,
                app_user = AppUser,
                app_time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                amount = (int)amount,
                app_trans_id = DateTime.Now.ToString("yyMMdd") + "_" + new Random().Next(1000, 9999),
                embed_data = embedData,
                item = items,
                description = $"Thanh toán đơn hàng #{orderId}",
                bank_code = ""
            };


            // Tính toán signature
            var dataToHash = $"{AppId}|{payload.app_trans_id}|{payload.app_user}|{payload.amount}|{payload.app_time}|{embedData}|{items}";
            var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(Key1));
            payload.signature = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToHash))).Replace("-", "").ToLower();

            // Gửi yêu cầu đến ZaloPay API
            using var httpClient = new HttpClient();
            var jsonContent = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ZaloPayApiUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(ZaloPayApiUrl);


            // Xử lý phản hồi từ ZaloPay
            var result = JsonConvert.DeserializeObject<CreateZalopayPayResponse>(responseBody);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating ZaloPay order: " + ex.Message, ex);
        }
    }
}

public class ZaloPayPayload
{
    public string app_id { get; set; }
    public string app_user { get; set; }
    public long app_time { get; set; }
    public int amount { get; set; }
    public string app_trans_id { get; set; }
    public string embed_data { get; set; }
    public string item { get; set; }
    public string description { get; set; }
    public string bank_code { get; set; }
    public string signature { get; set; }
}

public class CreateZalopayPayResponse
{
    public int ReturnCode { get; set; }
    public string ReturnMessage { get; set; } = string.Empty;
    public string OrderUrl { get; set; } = string.Empty;
}
}