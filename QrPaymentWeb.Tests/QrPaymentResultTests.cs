
using QrPaymentWeb.Enums;
using QrPaymentWeb.Models;
using Xunit;

namespace QrPaymentWeb.Tests
{
    public class QrPaymentResultTests
    {
        [Fact]
        public void Success_WhenPaymentIsValid_ReturnsSuccessfulResult()
        {
            // Arrange
            var payment = new QrPaymentResponse
            {
                QrId = "ABC123XYZ",
                Recipient = "Test Store",
                Amount = 250.00m,
                Currency = "TJS",
                Status = "Active"
            };

            // Act
            var result = QrPaymentResult.Success(payment);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Payment);
            Assert.Equal("ABC123XYZ", result.Payment.QrId);
            Assert.Equal(QrPaymentError.None, result.Error);
        }

        [Fact]
        public void Success_WhenPaymentIsNull_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => QrPaymentResult.Success(null!)
            );
        }

        [Fact]
        public void Failure_WhenErrorIsNotNone_ReturnsFailedResult()
        {
            // Act
            var result = QrPaymentResult.Failure(QrPaymentError.NotFound);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Payment);
            Assert.Equal(QrPaymentError.NotFound, result.Error);
        }

        [Fact]
        public void Failure_WhenErrorIsNone_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => QrPaymentResult.Failure(QrPaymentError.None)
            );
        }
    }
}
