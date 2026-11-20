
using Microsoft.AspNetCore.Mvc;
using QrPaymentWeb.Enums;
using QrPaymentWeb.Services.Interfaces;
using QrPaymentWeb.ViewModels;
using QrPaymentWeb.Validators;

namespace QrPaymentWeb.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IQrPaymentService _qrPaymentService;

        public PaymentController(IQrPaymentService qrPaymentService)
        {
            _qrPaymentService = qrPaymentService;
        }

        [HttpGet("/pay/{qrId?}")]
        public async Task<IActionResult> Index(string? qrId)
        {
            // 1. Проверяем QR-ID
            var validationError = QrIdValidator.GetError(qrId);

            if (validationError != null)
            {
                var errorModel = new PaymentErrorViewModel();

                switch (validationError)
                {
                    case "EMPTY":
                        errorModel.Title = "QR-код отсутствует";
                        errorModel.Message =
                            "Не удалось получить идентификатор QR-кода. Отсканируйте QR-код повторно.";
                        break;

                    case "TOO_LONG":
                        errorModel.Title = "Некорректный QR-код";
                        errorModel.Message =
                            "Длина идентификатора превышает допустимое значение.";
                        break;

                    case "INVALID_FORMAT":
                        errorModel.Title = "Некорректный QR-код";
                        errorModel.Message =
                            "QR-код содержит недопустимые символы.";
                        break;
                }

                Response.StatusCode = StatusCodes.Status400BadRequest;

                return View("InvalidQr", errorModel);
            }

            // 2. Получаем результат от QR-сервиса
            var result = await _qrPaymentService.GetPaymentAsync(qrId!);

            // 3. Проверяем результат
            if (!result.IsSuccess)
            {
                var errorModel = new PaymentErrorViewModel();

                switch (result.Error)
                {
                    case QrPaymentError.NotFound:
                        Response.StatusCode = StatusCodes.Status404NotFound;

                        errorModel.Title = "QR-код не найден";
                        errorModel.Message =
                            "Платёж по данному QR-коду не найден. Проверьте QR-код или обратитесь к продавцу.";

                        return View("NotFound", errorModel);

                    case QrPaymentError.Timeout:
                    case QrPaymentError.ServiceUnavailable:
                        Response.StatusCode =
                            StatusCodes.Status503ServiceUnavailable;

                        errorModel.Title = "Сервис временно недоступен";
                        errorModel.Message =
                            "Не удалось получить информацию о платеже. Попробуйте немного позже.";

                        return View("ServiceUnavailable", errorModel);

                    default:
                        Response.StatusCode =
                            StatusCodes.Status500InternalServerError;

                        errorModel.Title = "Произошла ошибка";
                        errorModel.Message =
                            "Не удалось обработать запрос. Попробуйте позже.";

                        return View("ServiceUnavailable", errorModel);
                }
            }

            // 4. Получаем данные платежа
            var payment = result.Payment!;

            // 5. Создаём ViewModel
            var viewModel = new PaymentViewModel
            {
                QrId = payment.QrId,
                Recipient = payment.Recipient,
                Amount = payment.Amount,
                Currency = payment.Currency
            };

            return View(viewModel);
        }
    }
}
