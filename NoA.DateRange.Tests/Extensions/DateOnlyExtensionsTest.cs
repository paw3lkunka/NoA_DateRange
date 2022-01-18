using NoA.DateRange.Extensions;

namespace NoA.DateRange.Tests.Extensions; 

public class DateOnlyExtensionsTest {
  [Fact]
  public void MultiCultureParse_NullString_ThrowsArgumentNullException() {
    // Arrange
    DateOnly dateOnly = new DateOnly();
    // Act
    Action dateOnlyMultiCultureParse = () => dateOnly.MultiCultureParse(null!);
    // Assert
    Assert.Throws<ArgumentNullException>(dateOnlyMultiCultureParse);
  }

  [Theory]
  [InlineData("")]
  [InlineData("12345678")]
  public void MultiCultureParse_InvalidString_ThrowsFormatException(string dateString) {
    // Arrange
    DateOnly dateOnly = new DateOnly();
    // Act
    Action dateOnlyMultiCultureParse = () => dateOnly.MultiCultureParse(dateString);
    // Assert
    Assert.Throws<FormatException>(dateOnlyMultiCultureParse);
  }

  public static IEnumerable<object[]> Data_MultiCultureParse_ValidDateString_ReturnsExpectedResult = new List<object[]> {
    new object[] {"0001-01-01", new DateOnly(1, 1, 1)},
    new object[] {"12/31/2000", new DateOnly(2000, 12, 31)},
    new object[] {"31.12.2000", new DateOnly(2000, 12, 31)},
  };

  [Theory]
  [MemberData(nameof(Data_MultiCultureParse_ValidDateString_ReturnsExpectedResult))]
  public void MultiCultureParse_ValidDateString_ReturnsExpectedResult(string dateString, DateOnly expectedResult) {
    // Act
    DateOnly result = new DateOnly().MultiCultureParse(dateString);
    // Assert
    Assert.Equal(expectedResult, result);
  }
}