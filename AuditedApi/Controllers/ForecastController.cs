using Audit.WebApi;
using AuditedApi.Models;
using AuditedApi.Services;
using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AuditedApi.Contollers
{

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
            var CorrelationId = _correlationContext.CorrelationContext.CorrelationId;
            _logger.LogInformation("Getting forecasts: correlationId={CorrelationId}", CorrelationId);

            var forecast = _forecastService.GetForecast(DateTime.Now);
            return forecast;
        }
    }
}
