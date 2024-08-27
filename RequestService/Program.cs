using System.Net;
using Polly;
using Polly.Retry;
using Polly.Simmy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddHttpClient("ClientService")
    .AddResilienceHandler("my-pipeline", (ResiliencePipelineBuilder<HttpResponseMessage> builder) =>
{
    builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage> { MaxRetryAttempts = 3 });   
    const double failureRate = 0.5;
    builder.AddChaosOutcome(failureRate, () => new HttpResponseMessage(HttpStatusCode.InternalServerError));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
