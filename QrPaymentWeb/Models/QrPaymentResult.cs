using QrPaymentWeb.Enums;

namespace QrPaymentWeb.Models
{
    public class QrPaymentResult
    {
        public bool IsSuccess { get; private set; }

        public QrPaymentResponse? Payment { get; private set; }

        public QrPaymentError Error { get; private set; }

        private QrPaymentResult()
        {
        }

        public static QrPaymentResult Success(
            QrPaymentResponse payment)
        {
            ArgumentNullException.ThrowIfNull(payment);

            return new QrPaymentResult
            {
                IsSuccess = true,
                Payment = payment,
                Error = QrPaymentError.None
            };
        }

        public static QrPaymentResult Failure(
            QrPaymentError error)
        {
            if (error == QrPaymentError.None)
                throw new ArgumentException(
                    "Failure cannot have None error.");

            return new QrPaymentResult
            {
                IsSuccess = false,
                Payment = null,
                Error = error
            };
        }
    }
}
