using Xunit;
using NoA.DateRange.Services;

namespace NoA.DateRange.Tests; 

public class DateRangeServiceTest {
  public static IEnumerable<object[]> Data_CreateString_ValidInput_ValidResult = new List<object[]>() {
    new object[] {new DateOnly(0001, 01, 01), new DateOnly(0001, 01, 31), "01 - 31.01.0001"},
    new object[] {new DateOnly(0001, 01, 01), new DateOnly(0001, 12, 31), "01.01 - 31.12.0001"},
    new object[] {new DateOnly(0001, 01, 01), new DateOnly(9999, 12, 31), "01.01.0001 - 31.12.9999"},
  };

  [Theory]
  [MemberData(nameof(Data_CreateString_ValidInput_ValidResult))]
  public void CreateString_ValidInput_ValidResult(DateOnly startDate, DateOnly endDate, string expectedResult) {
    // Arrange
    IDateRangeService service = new DateRangeService();
    // Act
    string result = service.CreateString(startDate, endDate);
    // Assert
    Assert.Equal(result, expectedResult);
  }

  [Fact]
  public void CreateString_StartDateGreaterThanEndDate_ThrowsArgumentException() {
    // Arrange
    IDateRangeService service = new DateRangeService();
    DateOnly startDate = new DateOnly(9999, 12, 31);
    DateOnly endDate = new DateOnly(0001, 01, 01);
    // Act
    Action createStringAction = () => service.CreateString(startDate, endDate);
    // Assert
    Assert.Throws<ArgumentException>(createStringAction);
  }
}