using System.Net;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http.Resilience;
using Polly;
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

var httpClientBuilder = builder.Services.AddHttpClient("ClientService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
    client.DefaultRequestHeaders.Add("X-Correlation-Id", Guid.NewGuid().ToString());
});

httpClientBuilder.AddStandardResilienceHandler()
    .Configure(options =>
    {
        options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(1);

        options.CircuitBreaker.ShouldHandle = args => args.Outcome switch
        {
            { } outcome when HttpClientResiliencePredicates.IsTransient(outcome) => PredicateResult.True(),
            { Exception: InvalidOperationException } => PredicateResult.True(),
            _ => PredicateResult.False(),
        };

        options.Retry.ShouldHandle = args => args.Outcome switch
        {
            { } outcome when HttpClientResiliencePredicates.IsTransient(outcome) => PredicateResult.True(),
            { Exception: InvalidOperationException } => PredicateResult.True(),
            _ => PredicateResult.False(),
        };
    });

httpClientBuilder
    .AddResilienceHandler("chaos", (builder, context) =>
{
    var chaosManager = context.ServiceProvider.GetRequiredService<IChaosManager>();

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
