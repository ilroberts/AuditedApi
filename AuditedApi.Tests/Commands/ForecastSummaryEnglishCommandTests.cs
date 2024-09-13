using AuditedApi.Commands;

namespace AuditedApi.Tests.Commands
{
    public class ForecastSummaryEnglishCommandTests
    {
        private static readonly string[] s_expectedEnglishSummaries = [
                "Freezing", "Bracing", "Chilly", "Cool", "Mild",
                "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            ];

        [Fact]
        public void GetForecastSummary_ReturnsValidOption()
        {
            // Arrange
            var command = new ForecastSummaryEnglishCommand();

            // Act
            string summary = command.GetForecastSummary();

            // Assert
            Assert.Contains(summary, s_expectedEnglishSummaries);
        }
    }
}

