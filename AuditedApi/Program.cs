using Audit.WebApi;
using AuditedApi.Commands;
using AuditedApi.Services;
using CorrelationId;
using CorrelationId.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultCorrelationId();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddMvc(options => options.Filters.Add(new AuditApiAttribute()));

builder.Services.AddSingleton<IForecastService, ForecastService>();

builder.Services.AddSingleton<IForecastSummaryCommand>(provider =>
{
    var language = provider.GetRequiredService<IConfiguration>()["Language"];
    return language switch
    {
        "fr" => ActivatorUtilities.CreateInstance<ForecastSummaryFrenchCommand>(provider),
        "en" => ActivatorUtilities.CreateInstance<ForecastSummaryEnglishCommand>(provider),
        _ => throw new ArgumentException($"Language '{language}' not supported")
    };
});

Audit.Core.Configuration.Setup()
    .UseFileLogProvider(cfg => cfg.Directory(@"logs"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuditedApi v1"));
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseCorrelationId();

app.Run();
