namespace AuditedApi.Tests.Services;

using System;
using AuditedApi.Services;
using Xunit;

public class ForecastServiceTests
{
  private readonly ForecastService _service;

  public ForecastServiceTests() => this._service = new ForecastService();

  [Fact]
  public void GetForecastReturnsWeatherForecasts()
  {
    // Arrange
    var startDate = DateTime.Now;

    // Act
    var result = this._service.GetForecast(startDate);

    // Assert
    Assert.Equal(5, result.Length);
  }
}
