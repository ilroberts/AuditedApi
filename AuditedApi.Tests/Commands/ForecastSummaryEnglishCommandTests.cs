namespace AuditedApi.Tests.Commands
{
    using AuditedApi.Commands;

    public class ForecastSummaryEnglishCommandTests
    {

        [Fact]
        public void GetForecastSummaryReturnsValidOptions()
        {
            // Arrange
            var command = new ForecastSummaryEnglishCommand();
            // Act
            var summary = command.GetForecastSummary();
            // Assert
            Assert.Contains(summary, new string[] {
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
            });
        }
    }
}

