using AuditedApi.Models;

namespace AuditedApi.Services
{
    public interface IForecastService
    {
        WeatherForecast[] GetForecast(DateTime startDate);
    }
}
