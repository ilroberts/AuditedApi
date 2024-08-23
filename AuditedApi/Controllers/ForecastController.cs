using Audit.WebApi;
using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AuditedApi.Contollers
{
    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    [Route("api/[controller]")]
    [ApiController]
    [AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeHeaders = true)]
    public class ForecastController(ILogger<ForecastController> logger,
        ICorrelationContextAccessor correlationContext) : ControllerBase
    {
        private readonly ILogger<ForecastController> _logger = logger;
        private readonly ICorrelationContextAccessor _correlationContext = correlationContext;

        private readonly string[] summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet]
        public WeatherForecast[] GetForecasts()
        {
            var CorrelationId = _correlationContext.CorrelationContext.CorrelationId;
            _logger.LogInformation("Getting forecasts: correlationId={CorrelationId}", CorrelationId);

            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                )).ToArray();

            return forecast;
        }
    }
}
