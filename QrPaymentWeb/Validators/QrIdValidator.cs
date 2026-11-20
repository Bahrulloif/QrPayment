
using System.Text.RegularExpressions;

namespace QrPaymentWeb.Validators
{
    public static class QrIdValidator
    {
        public static string? GetError(string? qrId)
        {
            if (string.IsNullOrWhiteSpace(qrId))
                return "EMPTY";

            if (qrId.Length > 64)
                return "TOO_LONG";

            if (!Regex.IsMatch(qrId, @"\A[A-Za-z0-9_-]+\z"))
                return "INVALID_FORMAT";

            return null;
        }
    }
}
