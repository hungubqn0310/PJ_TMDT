using _123.Models;
using _123.Models.Momo;
namespace _123.Services.Momo
{
    public interface IMomoService{
        Task<MomoCreatePaymentResponseModel> CreatePaymentMomo(OrderInfoModel model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);

    }
}