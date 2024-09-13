using AuditedApi.Commands;

namespace AuditedApi.Tests.Commands
{
    public class ForecastSummaryFrenchCommandTests
    {
        private static readonly string[] s_expectedFrenchSummaries =
{
            "Gelé", "Vif", "Frais", "Doux", "Chaud",
            "Clément", "Étouffant", "Accablant"
        };

        [Fact]
        public void GetForecastSummaryReturnsValidOptions()
        {
            // Arrange
            var command = new ForecastSummaryFrenchCommand();

            // Act
            string summary = command.GetForecastSummary();

            // Assert
            Assert.Contains(summary, s_expectedFrenchSummaries);
        }
    }
}

