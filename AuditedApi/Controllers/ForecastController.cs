using Audit.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditedApi.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        [HttpGet]
        [AuditApi(EventTypeName = "GetForecasts")]
        public string GetForecasts()
        {
            return "Forecasts";
        }
    }
}
