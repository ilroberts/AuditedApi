namespace AuditedApi.Tests.Services
{
    using AuditedApi.Models;
    using AuditedApi.Services;
    using System;
    using Xunit;

    public class ForecastServiceTests
    {
        private readonly ForecastService _service;

        public ForecastServiceTests()
        {
            _service = new ForecastService();
        }

        [Fact]
        public void GetForecast_ReturnsWeatherForecasts()
        {
            // Arrange
            DateTime startDate = DateTime.Now;

            // Act
            WeatherForecast[] result = _service.GetForecast(startDate);

            // Assert
            Assert.Equal(5, result.Length);
        }
    }
}
