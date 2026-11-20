
namespace QrPaymentWeb.Models
{
    public class QrPaymentResponse
    {
        public string QrId { get; set; } = string.Empty;

        public string Recipient { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "TJS";

        public string Status { get; set; } = string.Empty;
    }
}
