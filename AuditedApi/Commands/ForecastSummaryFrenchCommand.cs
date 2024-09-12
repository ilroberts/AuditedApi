namespace AuditedApi.Commands
{
    public class ForecastSummaryFrenchCommand : IForecastSummaryCommand
    {
        public string GetForecastSummary()
        {
            return GetRandomSummary();
        }

        private static string GetRandomSummary()
        {
            string[] summaries =
            {
                "Gelé",
                "Vif",
                "Frais",
                "Frais",
                "Doux",
                "Chaud",
                "Clément",
                "Chaud",
                "Étouffant",
                "Accablant"
            };

            return summaries[new Random().Next(summaries.Length)];
        }
    }
}
