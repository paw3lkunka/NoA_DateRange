namespace NoA.DateRange.Services;

public interface IDateRangeService {
  /// <summary>
  /// <p>Creates shorthand string representation of date range as follows:</p>
  /// <ul>
  ///   <li><b>dd - dd.MM.yyyy</b> - when year and month is shared.</li>
  ///   <li><b>dd.MM - dd.MM.yyyy</b> - when year is shared.</li>
  ///   <li><b>dd.MM.yyyy - dd.MM.yyyy</b> - in all other cases.</li>
  /// </ul>
  /// </summary>
  /// <param name="startDate">Start date of date range</param>
  /// <param name="endDate">End date of date range</param>
  /// <returns>String that represents date range</returns>
  string CreateString(DateOnly startDate, DateOnly endDate);

  /// <inheritdoc cref="CreateString"/>
  string CreateString(string startDate, string endDate);
}