using NoA.DateRange.Services;

namespace NoA.DateRange.Tests.Services;

public class DateRangeServiceTest {
  public static IEnumerable<object[]> Data_CreateString_ValidInput_ValidResult = new List<object[]>() {
    new object[] {DateOnly.MinValue, new DateOnly(0001, 01, 31), "01/01 - 31/0001"},
    new object[] {DateOnly.MinValue, new DateOnly(0001, 12, 31), "01/01 - 12/31/0001"},
    new object[] {DateOnly.MinValue, DateOnly.MaxValue, "01/01/0001 - 12/31/9999"},
  };

  [Theory]
  [MemberData(nameof(Data_CreateString_ValidInput_ValidResult))]
  public async Task CreateString_ValidInput_ValidResult(DateOnly startDate, DateOnly endDate, string expectedResult) {
    // Arrange
    IDateRangeService service = new DateRangeService();
    await Task.Run(() => {
      CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
      // Act
      string result = service.CreateString(startDate, endDate);
      // Assert
      Assert.Equal(expectedResult, result);
    });
  }

  [Fact]
  public async Task CreateString_StartDateGreaterThanEndDate_ThrowsArgumentException() {
    // Arrange
    IDateRangeService service = new DateRangeService();
    DateOnly startDate = DateOnly.MaxValue;
    DateOnly endDate = DateOnly.MinValue;
    await Task.Run(() => {
      CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
      // Act
      Action createStringAction = () => service.CreateString(startDate, endDate);
      // Assert
      Assert.Throws<ArgumentException>(createStringAction);
    });
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
  [InlineData("en_US", "04/04/2000", "04/07/2000", "4/4 - 7/2000")]
  [InlineData("pl_PL", "04/07/2000", "07/07/2000", "04 - 07.07.2000")]
  [InlineData("af-NA", "2000-07-04", "2000-07-07", "2000-07-04 - 07")]
  [InlineData("en_US", "04/07/2000", "07/04/2000", "4/7 - 7/4/2000")]
  [InlineData("pl_PL", "07/04/2000", "04/07/2000", "07.04 - 04.07.2000")]
  [InlineData("af-NA", "2000-04-07", "2000-07-04", "2000-04-07 - 07-04")]
  [InlineData("en_US", "04/07/2000", "07/04/2001", "4/7/2000 - 7/4/2001")]
  [InlineData("pl_PL", "07/04/2000", "04/07/2001", "07.04.2000 - 04.07.2001")]
  [InlineData("af-NA", "2000-04-07", "2001-07-04", "2000-04-07 - 2001-07-04")]
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