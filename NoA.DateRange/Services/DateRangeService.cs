namespace NoA.DateRange.Services; 

/// <inheritdoc/>
public class DateRangeService : IDateRangeService {
  /// <inheritdoc/>
  /// <exception cref="ArgumentException">startDate is greater than or equal endDate</exception>
  public string CreateString(DateOnly startDate, DateOnly endDate) {
    if (startDate >= endDate) {
      throw new ArgumentException("Start date must be an earlier date than end date");
    }
    // Check if years and/or months are equal
    if (startDate.Year == endDate.Year) {
      if (startDate.Month == endDate.Month) {
        return $"{startDate.ToString("dd")} - {endDate.ToString("dd.MM.yyyy")}";
      }
      else {
        return $"{startDate.ToString("dd.MM")} - {endDate.ToString("dd.MM.yyyy")}";
      }
    }
    else {
      return $"{startDate.ToString("dd.MM.yyyy")} - {endDate.ToString("dd.MM.yyyy")}";
    }
  }

  /// <inheritdoc cref="CreateString(System.DateOnly,System.DateOnly)"/>
  /// <exception cref="ArgumentNullException">startDate or endDate is null</exception>
  /// <exception cref="FormatException">startDate or endDate does not contain a valid string representation of a date.</exception>
  public string CreateString(string startDate, string endDate) {
    DateOnly parsedStartDate = DateOnly.Parse(startDate);
    DateOnly parsedEndDate = DateOnly.Parse(endDate);
    return CreateString(parsedStartDate, parsedEndDate);
  }
}