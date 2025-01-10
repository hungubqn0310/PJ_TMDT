using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Collections.Generic;
using ZaloPay.Helper; // HmacHelper, RSAHelper, HttpHelper, Utils (tải về ở mục DOWNLOADS)
using ZaloPay.Helper.Crypto;

namespace _123.Services
{
    public class ZaloPayService
    {
        private readonly HttpClient _httpClient;

        static string app_id = "2553";
        static string key1 = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL";
        static string create_order_url = "https://sb-openapi.zalopay.vn/v2/create";

        public ZaloPayService()
        {
            _httpClient = new HttpClient();
        }

        // Hàm tạo yêu cầu thanh toán
        public static async Task<Dictionary<string, string>> CreatePaymentRequestAsync(decimal amount, string description)
        {
            Random rnd = new Random();
            var embed_data = new { redirecturl = "https://localhost:5003/home/paymentSuccessful" };
            var items = new[] { new { } };
            var param = new Dictionary<string, string>();
            var app_trans_id = rnd.Next(1000000); // Generate a random order's ID.

            param.Add("app_id", app_id);
            param.Add("app_user", "user123");
            param.Add("app_time", Utils.GetTimeStamp().ToString());
            param.Add("amount", amount.ToString("F0"));
            param.Add("app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id); // mã giao dich có định dạng yyMMdd_xxxx
            param.Add("embed_data", JsonConvert.SerializeObject(embed_data));
            param.Add("item", JsonConvert.SerializeObject(items));
            param.Add("description", description);
            param.Add("bank_code", "");

            var data = app_id + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|" 
                + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            var result = await HttpHelper.PostFormAsync(create_order_url, param);

            var stringResult = result.ToDictionary(
                entry => entry.Key,
                entry => entry.Value?.ToString() ?? string.Empty
            );

            return stringResult;
        }
    }
}
