using System;
using AuditedApi.Commands;
using AuditedApi.Models;

namespace AuditedApi.Services;

public class ForecastService(IForecastSummaryCommand summaryCommand,
    IHttpContextAccessor httpContextAccessor,
    ILogger<ForecastService> logger) : IForecastService
{
    private readonly IForecastSummaryCommand _summaryCommand = summaryCommand;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ILogger _logger = logger;

    public WeatherForecast[] GetForecast(DateTime startDate)
    {
        // get the correlation id as an example of how to use IHttpContextAccessor
        var correlationId = _httpContextAccessor.HttpContext?.Request?.Headers["X-Correlation-ID"].FirstOrDefault();
        _logger.LogInformation("Correlation ID: {CorrelationId}", correlationId ?? "(not set)");

        return Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(startDate.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaryCommand.GetForecastSummary()
            )).ToArray();
    }
}
