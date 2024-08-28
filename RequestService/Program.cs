using System.Net;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;
using Polly.Retry;
using Polly.Simmy;
using Polly.Simmy.Fault;
using Polly.Simmy.Latency;
using Polly.Simmy.Outcomes;
using RequestService.Chaos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.TryAddSingleton<IChaosManager, ChaosManager>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("ClientService")
    .AddResilienceHandler("my-pipeline", (builder, context) =>
{
    var chaosManager = context.ServiceProvider.GetRequiredService<IChaosManager>();

    builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
    {
        MaxRetryAttempts = 5,
        ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
            .HandleResult(response => !response.IsSuccessStatusCode)
    });

    builder
    .AddChaosLatency(new ChaosLatencyStrategyOptions
    {
        EnabledGenerator = args => chaosManager.IsChaosEnabledAsync(args.Context),
        InjectionRateGenerator = args => chaosManager.GetInjectionRateAsync(args.Context),
        Latency = TimeSpan.FromSeconds(5)
    })
    .AddChaosOutcome(new ChaosOutcomeStrategyOptions<HttpResponseMessage>
    {
        EnabledGenerator = args => chaosManager.IsChaosEnabledAsync(args.Context),
        InjectionRateGenerator = args => chaosManager.GetInjectionRateAsync(args.Context),
        OutcomeGenerator = new OutcomeGenerator<HttpResponseMessage>()
            .AddResult(() => new HttpResponseMessage(HttpStatusCode.InternalServerError))
    }).AddChaosFault(new ChaosFaultStrategyOptions
    {
        EnabledGenerator = args => chaosManager.IsChaosEnabledAsync(args.Context),
        InjectionRateGenerator = args => chaosManager.GetInjectionRateAsync(args.Context),
        FaultGenerator = new FaultGenerator()
            .AddException(() => new InvalidOperationException("chaos strategy injection!"))
    });
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
