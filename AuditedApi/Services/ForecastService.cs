namespace AuditedApi.Services;

using System;
using System.Linq;
using AuditedApi.Commands;
using AuditedApi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

public class ForecastService : IForecastService
{
    private readonly IForecastSummaryCommand _summaryCommand;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger _logger;

    public ForecastService(IForecastSummaryCommand summaryCommand,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ForecastService> logger)
    {
        _summaryCommand = summaryCommand;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public WeatherForecast[] GetForecast(DateTime startDate)
    {
        // get the correlation id as an example of how to use IHttpContextAccessor
        var correlationId = _httpContextAccessor.HttpContext.Request.Headers["X-Correlation-ID"];
        _logger.LogInformation($"Correlation ID: {correlationId}");

        return Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(startDate.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaryCommand.GetForecastSummary()
            )).ToArray();
    }
}
