namespace AuditedApi.Tests.Commands
{
    using AuditedApi.Commands;

    public class ForecastSummaryFrenchCommandTests
    {

        [Fact]
        public void GetForecastSummaryReturnsValidOptions()
        {
            // Arrange
            var command = new ForecastSummaryFrenchCommand();
            // Act
            var summary = command.GetForecastSummary();
            // Assert
            Assert.Contains(summary, new string[] {
                "Gelé",
                "Vif",
                "Frais",
                "Doux",
                "Chaud",
                "Clément",
                "Chaud",
                "Étouffant",
                "Accablant"
            });
        }
    }
}

