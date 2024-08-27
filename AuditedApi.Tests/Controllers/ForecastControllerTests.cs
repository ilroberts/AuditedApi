using AuditedApi.Contollers;
using AuditedApi.Models;
using AuditedApi.Services;
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
        private readonly Mock<IForecastService> _mockForecastService;

        public ForecastControllerTests()
        {
            _mockLogger = new Mock<ILogger<ForecastController>>();
            _mockCorrelationContextAccessor = new Mock<ICorrelationContextAccessor>();
            _mockForecastService = new Mock<IForecastService>();

            _controller = new ForecastController(_mockLogger.Object,
                _mockCorrelationContextAccessor.Object,
                _mockForecastService.Object);
        }

        [Fact]
        public void GetForecasts_ReturnsWeatherForecasts()
        {
            // Arrange
            _mockCorrelationContextAccessor.SetupGet(static x => x.CorrelationContext)
                .Returns(new CorrelationContext("ABC-123", "RequestHeader"));
            _mockForecastService.Setup(static x => x.GetForecast(It.IsAny<DateTime>()))
                .Returns(CreateWeatherForecasts());

            // Act
            var result = _controller.GetForecasts();

            // Assert
            Assert.Single(result);
        }

        private static WeatherForecast[] CreateWeatherForecasts()
        {
            WeatherForecast forecast = new(
                DateOnly.FromDateTime(DateTime.Now),
                20,
                "Warm"
            );

            return [forecast];
        }
    }
}
