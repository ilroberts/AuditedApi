using AuditedApi.Contollers;
using CorrelationId;
using CorrelationId.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AuditedApi.Controllers.Tests
{
    public class ForecastControllerTests
    {
        private readonly Mock<ILogger<ForecastController>> _mockLogger;
        private readonly Mock<ICorrelationContextAccessor> _mockCorrelationContextAccessor;
        private readonly ForecastController _controller;

        public ForecastControllerTests()
        {
            _mockLogger = new Mock<ILogger<ForecastController>>();
            _mockCorrelationContextAccessor = new Mock<ICorrelationContextAccessor>();

            _controller = new ForecastController(_mockLogger.Object, _mockCorrelationContextAccessor.Object);
        }

        [Fact]
        public void GetForecasts_ReturnsWeatherForecasts()
        {
            // Arrange
            _mockCorrelationContextAccessor.SetupGet(static x => x.CorrelationContext)
                .Returns(new CorrelationContext("ABC-123", "RequestHeader"));

            // Act
            var result = _controller.GetForecasts();

            // Assert
            Assert.Equal(5, result.Length);
        }
    }
}
