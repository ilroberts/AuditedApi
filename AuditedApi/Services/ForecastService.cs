namespace AuditedApi.Services;

using System;
using System.Linq;
using AuditedApi.Models;

public class ForecastService : IForecastService
{
  private readonly string[] summaries =
  [
      "Freezing",
    "Bracing",
    "Chilly",
    "Cool",
    "Mild",
    "Warm",
    "Balmy",
    "Hot",
    "Sweltering",
    "Scorching"
  ];

  public WeatherForecast[] GetForecast(DateTime startDate) => Enumerable.Range(1, 5).Select(index =>
                                                                   new WeatherForecast
                                                                   (
                                                                       DateOnly.FromDateTime(startDate.AddDays(index)),
                                                                       Random.Shared.Next(-20, 55),
                                                                       this.summaries[Random.Shared.Next(this.summaries.Length)]
                                                                   )).ToArray();
}
