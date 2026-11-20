using QrPaymentWeb.Models;

namespace QrPaymentWeb.Services.Interfaces
{
    public interface IQrPaymentService
    {
        Task<QrPaymentResult> GetPaymentAsync(string qrId);
    }
}