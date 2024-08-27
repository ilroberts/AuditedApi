namespace AuditedApi.Services
{
    using AuditedApi.Models;
    using System;
    using System.Linq;

    public class ForecastService : IForecastService
    {
        private readonly string[] summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        public WeatherForecast[] GetForecast(DateTime startDate)
        {
            return Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(startDate.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                )).ToArray();
        }
    }
}
