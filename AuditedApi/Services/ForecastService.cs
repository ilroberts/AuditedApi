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

    public ForecastService(IForecastSummaryCommand summaryCommand)
    {
        _summaryCommand = summaryCommand;
    }

    public WeatherForecast[] GetForecast(DateTime startDate)
    {
        return Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(startDate.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaryCommand.GetForecastSummary()
            )).ToArray();
    }
}
