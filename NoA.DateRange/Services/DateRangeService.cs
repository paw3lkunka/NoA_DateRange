namespace NoA.DateRange.Services; 

public class DateRangeService : IDateRangeService {
  /// <inheritdoc/>
  public string CreateString(DateOnly startDate, DateOnly endDate) {
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

  /// <inheritdoc/>
  public string CreateString(string startDate, string endDate) {
    throw new NotImplementedException();
  }
}