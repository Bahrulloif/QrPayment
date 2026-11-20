
using QrPaymentWeb.Models;
using QrPaymentWeb.Services.Interfaces;
using QrPaymentWeb.Enums;

namespace QrPaymentWeb.Services.Implementations
{
    public class MockQrPaymentService : IQrPaymentService
    {
        public Task<QrPaymentResult> GetPaymentAsync(string qrId)
        {
            var payments = new List<QrPaymentResponse>
            {
                new QrPaymentResponse
                {
                    QrId = "123456789",
                    Recipient = "ABC Supermarket",
                    Amount = 150.00m,
                    Currency = "TJS",
                    Status = "Active"
                },

                new QrPaymentResponse
                {
                    QrId = "ABC123XYZ",
                    Recipient = "Test Store",
                    Amount = 250.00m,
                    Currency = "TJS",
                    Status = "Active"
                }
            };

            var payment = payments.FirstOrDefault(
                p => p.QrId == qrId);

            if (payment == null)
            {
                return Task.FromResult(
                    QrPaymentResult.Failure(
                        QrPaymentError.NotFound));
            }

            return Task.FromResult(
                QrPaymentResult.Success(payment));
        }
    }
}
