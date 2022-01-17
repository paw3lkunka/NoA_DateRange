using System.Globalization;
using System.Threading.Tasks;
using Xunit;
using NoA.DateRange.Services;

namespace NoA.DateRange.Tests; 

public class DateRangeServiceTest {
  public static IEnumerable<object[]> Data_CreateString_ValidInput_ValidResult = new List<object[]>() {
    new object[] {DateOnly.MinValue, new DateOnly(0001, 01, 31), "01 - 31.01.0001"},
    new object[] {DateOnly.MinValue, new DateOnly(0001, 12, 31), "01.01 - 31.12.0001"},
    new object[] {DateOnly.MinValue, DateOnly.MaxValue, "01.01.0001 - 31.12.9999"},
  };

  [Theory]
  [MemberData(nameof(Data_CreateString_ValidInput_ValidResult))]
  public void CreateString_ValidInput_ValidResult(DateOnly startDate, DateOnly endDate, string expectedResult) {
    // Arrange
    IDateRangeService service = new DateRangeService();
    // Act
    string result = service.CreateString(startDate, endDate);
    // Assert
    Assert.Equal(expectedResult, result);
  }

  [Fact]
  public void CreateString_StartDateGreaterThanEndDate_ThrowsArgumentException() {
    // Arrange
    IDateRangeService service = new DateRangeService();
    DateOnly startDate = DateOnly.MaxValue;
    DateOnly endDate = DateOnly.MinValue;
    // Act
    Action createStringAction = () => service.CreateString(startDate, endDate);
    // Assert
    Assert.Throws<ArgumentException>(createStringAction);
  }

  [Theory]
  [InlineData(null, "9999-12-31")]
  [InlineData("0001-01-01", null)]
  [InlineData(null, null)]
  public async Task CreateString_DateStringsNull_ThrowsArgumentNullException(string startDate, string endDate) {
    // Arrange
    IDateRangeService service = new DateRangeService();
    await Task.Run(() => {
      // Act
      CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
      Action createStringAction = () => service.CreateString(startDate, endDate);
      // Assert
      Assert.Throws<ArgumentNullException>(createStringAction);
    });
  }

  [Theory]
  [InlineData("12345678", "9999-12-31")]
  [InlineData("0001-01-01", "12345678")]
  [InlineData("12345678", "12345678")]
  public async Task CreateString_DateStringInvalidFormat_ThrowsFormatException(string startDate, string endDate) {
    // Arrange
    IDateRangeService service = new DateRangeService();
    await Task.Run(() => {
      // Act
      CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
      Action createStringAction = () => service.CreateString(startDate, endDate);
      // Assert
      Assert.Throws<FormatException>(createStringAction);
    });
  }
  
  [Theory]
  [InlineData("en_US", "04/07/2000", "07/04/2000", "07.04 - 04.07.2000")]
  [InlineData("pl_PL", "07/04/2000", "04/07/2000", "07.04 - 04.07.2000")]
  public async Task CreateString_DifferentCultures_ReturnsExpectedString(
      string cultureName, 
      string startDate, 
      string endDate, 
      string expectedResult) {
    // Arrange
    IDateRangeService service = new DateRangeService();
    await Task.Run(() => {
      // Act
      CultureInfo.CurrentCulture = new CultureInfo(cultureName);
      string result = service.CreateString(startDate, endDate);
      // Assert
      Assert.Equal(expectedResult, result);
    });
  }
}