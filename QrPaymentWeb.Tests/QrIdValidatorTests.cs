
using QrPaymentWeb.Validators;
using Xunit;

namespace QrPaymentWeb.Tests
{
    public class QrIdValidatorTests
    {
        [Fact]
        public void GetError_WhenQrIdIsNull_ReturnsEmpty()
        {
            // Arrange
            string? qrId = null;

            // Act
            var result = QrIdValidator.GetError(qrId);

            // Assert
            Assert.Equal("EMPTY", result);
        }

        [Fact]
        public void GetError_WhenQrIdIsValid_ReturnsNull()
        {
            // Arrange
            string qrId = "ABC123XYZ";

            // Act
            var result = QrIdValidator.GetError(qrId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetError_WhenQrIdContainsInvalidCharacters_ReturnsInvalidFormat()
        {
            // Arrange
            string qrId = "ABC@@@123";

            // Act
            var result = QrIdValidator.GetError(qrId);

            // Assert
            Assert.Equal("INVALID_FORMAT", result);
        }

        [Fact]
        public void GetError_WhenQrIdIsTooLong_ReturnsTooLong()
        {
            // Arrange
            string qrId = new string('A', 65);

            // Act
            var result = QrIdValidator.GetError(qrId);

            // Assert
            Assert.Equal("TOO_LONG", result);
        }

        [Fact]
        public void GetError_WhenQrIdContainsOnlySpaces_ReturnsEmpty()
        {
            // Arrange
            string qrId = "   ";

            // Act
            var result = QrIdValidator.GetError(qrId);

            // Assert
            Assert.Equal("EMPTY", result);
        }
    }
}
