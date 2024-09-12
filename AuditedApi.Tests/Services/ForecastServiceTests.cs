using System;
using AuditedApi.Services;
using AuditedApi.Commands;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;

namespace AuditedApi.Tests.Services;

public class ForecastServiceTests
{
    private readonly ForecastService _service;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    public ForecastServiceTests()
    {
        var logger = Mock.Of<Microsoft.Extensions.Logging.ILogger<ForecastService>>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var summaryCommand = new ForecastSummaryEnglishCommand();
        this._service = new ForecastService(summaryCommand, _mockHttpContextAccessor.Object, logger);
    }

    [Fact]
    public void GetForecastReturnsWeatherForecasts()
    {
        // Arrange
        var startDate = DateTime.Now;
        var context = new DefaultHttpContext();
        var request = context.Request;
        request.Headers["X-Correlation-ID"] = "1234";
        _mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

        // Act
        var result = this._service.GetForecast(startDate);

        // Assert
        Assert.Equal(5, result.Length);
    }
}
