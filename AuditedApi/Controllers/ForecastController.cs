using Audit.WebApi;
using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditedApi.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController(ILogger<ForecastController> logger, 
        ICorrelationContextAccessor correlationContext) : ControllerBase
    {
        private readonly ILogger<ForecastController> _logger = logger;
        private readonly ICorrelationContextAccessor _correlationContext = correlationContext;

        [HttpGet]
        [AuditApi(EventTypeName = "GetForecasts", IncludeHeaders = true)]
        public string GetForecasts()
        {
            var CorrelationId = _correlationContext.CorrelationContext.CorrelationId;
            _logger.LogInformation("Getting forecasts: correlationId={CorrelationId}", CorrelationId);
            return "Forecasts";
        }
    }
}
