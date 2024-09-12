namespace AuditedApi.Tests.Controllers;
using AuditedApi.Contollers;
using AuditedApi.Models;
using AuditedApi.Services;
using CorrelationId;
using CorrelationId.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;

public class ForecastControllerTests
{
  private readonly Mock<ILogger<ForecastController>> _mockLogger;
  private readonly Mock<ICorrelationContextAccessor> _mockCorrelationContextAccessor;
  private readonly ForecastController _controller;
  private readonly Mock<IForecastService> _mockForecastService;

  public ForecastControllerTests()
  {
    this._mockLogger = new Mock<ILogger<ForecastController>>();
    this._mockCorrelationContextAccessor = new Mock<ICorrelationContextAccessor>();
    this._mockForecastService = new Mock<IForecastService>();

    this._controller = new ForecastController(this._mockLogger.Object,
        this._mockCorrelationContextAccessor.Object,
        this._mockForecastService.Object);
  }

  [Fact]
  public void GetForecastsReturnsWeatherForecasts()
  {
    // Arrange
    this._mockCorrelationContextAccessor.SetupGet(static x => x.CorrelationContext)
        .Returns(new CorrelationContext("ABC-123", "RequestHeader"));
    this._mockForecastService.Setup(static x => x.GetForecast(It.IsAny<DateTime>()))
        .Returns(CreateWeatherForecasts());

    // Act
    var result = this._controller.GetForecasts();

    // Assert
    Assert.Single(result);
    Assert.Equal(20, result[0].TemperatureC);
    Assert.Equal("Warm", result[0].Summary);
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
