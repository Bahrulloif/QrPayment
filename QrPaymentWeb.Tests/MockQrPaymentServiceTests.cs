
using QrPaymentWeb.Enums;
using QrPaymentWeb.Services.Implementations;
using Xunit;

namespace QrPaymentWeb.Tests
{
    public class MockQrPaymentServiceTests
    {
        private readonly MockQrPaymentService _service;

        public MockQrPaymentServiceTests()
        {
            _service = new MockQrPaymentService();
        }

        [Fact]
        public async Task GetPaymentAsync_WhenQrIdExists_ReturnsPayment()
        {
            // Arrange
            string qrId = "123456789";

            // Act
            var result = await _service.GetPaymentAsync(qrId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Payment);
            Assert.Equal("ABC Supermarket", result.Payment.Recipient);
            Assert.Equal(150.00m, result.Payment.Amount);
        }

        [Fact]
        public async Task GetPaymentAsync_WhenSecondQrIdExists_ReturnsPayment()
        {
            // Arrange
            string qrId = "ABC123XYZ";

            // Act
            var result = await _service.GetPaymentAsync(qrId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Payment);
            Assert.Equal("Test Store", result.Payment.Recipient);
            Assert.Equal(250.00m, result.Payment.Amount);
        }

        [Fact]
        public async Task GetPaymentAsync_WhenQrIdDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            string qrId = "999999999";

            // Act
            var result = await _service.GetPaymentAsync(qrId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Payment);
            Assert.Equal(QrPaymentError.NotFound, result.Error);
        }
    }
}
