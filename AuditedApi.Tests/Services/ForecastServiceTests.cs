namespace AuditedApi.Tests.Services;

using System;
using AuditedApi.Services;
using AuditedApi.Commands;
using Xunit;

public class ForecastServiceTests
{
    private readonly ForecastService _service;

    public ForecastServiceTests()
    {
        var summaryCommand = new ForecastSummaryEnglishCommand();
        this._service = new ForecastService(summaryCommand);
    }

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
