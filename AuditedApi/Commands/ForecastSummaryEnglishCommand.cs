namespace AuditedApi.Commands
{
    public class ForecastSummaryEnglishCommand : IForecastSummaryCommand
    {
        public string GetForecastSummary()
        {
            return GetRandomSummary();
        }

        private static string GetRandomSummary()
        {
            string[] summaries =
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

            return summaries[new Random().Next(summaries.Length)];
        }
    }
}
