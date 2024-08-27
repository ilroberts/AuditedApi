using Audit.WebApi;
using AuditedApi.Services;
using CorrelationId;
using CorrelationId.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultCorrelationId();

builder.Services.AddControllers();
builder.Services.AddMvc(options => options.Filters.Add(new AuditApiAttribute()));

builder.Services.AddSingleton<IForecastService, ForecastService>();

Audit.Core.Configuration.Setup()
    .UseFileLogProvider(cfg => cfg.Directory(@"logs"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuditedApi v1");
    });
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseCorrelationId();

app.Run();
