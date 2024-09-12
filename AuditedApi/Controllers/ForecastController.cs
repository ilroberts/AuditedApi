namespace AuditedApi.Contollers;

using Audit.WebApi;
using AuditedApi.Models;
using AuditedApi.Services;
using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeHeaders = true)]
public class ForecastController(ILogger<ForecastController> logger,
    ICorrelationContextAccessor correlationContext,
    IForecastService forecastService) : ControllerBase
{
    private readonly ILogger<ForecastController> _logger = logger;
    private readonly ICorrelationContextAccessor _correlationContext = correlationContext;
    private readonly IForecastService _forecastService = forecastService;

    [HttpGet]
    public WeatherForecast[] GetForecasts()
    {
        var CorrelationId = this._correlationContext.CorrelationContext.CorrelationId;
        this._logger.LogInformation("Getting forecasts: correlationId={CorrelationId}", CorrelationId);

        var forecast = this._forecastService.GetForecast(DateTime.Now);
        return forecast;
    }
}
