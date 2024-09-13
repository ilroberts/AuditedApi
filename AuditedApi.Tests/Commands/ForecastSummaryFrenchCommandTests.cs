using AuditedApi.Commands;
using Xunit;

namespace AuditedApi.Tests.Commands;

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
        var command = new ForecastSummaryFrenchCommand();

        // Act
        string summary = command.GetForecastSummary();

        // Assert
        Assert.Contains(summary, s_expectedFrenchSummaries);
    }
}

