namespace AuditedApi.Tests;

public class LinqTests
{
  [Fact]
  public void WhenListOfThorinsCompanyIsFilteredThenNoDwarvesAreLeftInTheList()
  {
    // Arrange
    var dwarfList = new List<string> { "Bifur", "Bofur", "Bombur", "Thorin", "Dwalin", "Balin",
    "Kili", "Fili", "Dori", "Nori", "Ori", "Gloin", "Oin" };
    var others = new List<string> { "Bilbo", "Gandalf" };
    var theCompany = dwarfList.Concat(others).ToList();
    var expectedList = others;

    // Act
    var result = theCompany.Where(x => !dwarfList.Contains(x)).ToList();

    // Assert
    Assert.Equal(2, result.Count);
    Assert.True(result.All(expectedList.Contains));
  }
}
