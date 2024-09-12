namespace AuditedApi.Services;

using AuditedApi.Models;

public interface IForecastService
{
    WeatherForecast[] GetForecast(DateTime startDate);
}
