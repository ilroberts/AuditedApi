using Polly;

namespace RequestService.Chaos
{
    internal class ChaosManager(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor) : IChaosManager
    {
        public ValueTask<bool> IsChaosEnabledAsync(ResilienceContext context)
        {
            return ValueTask.FromResult(environment.IsDevelopment());
        }

        public ValueTask<double> GetInjectionRateAsync(ResilienceContext context)
        {
            return environment.IsDevelopment() ? ValueTask.FromResult(0.1) : ValueTask.FromResult(0.0);
        }
    }
}
