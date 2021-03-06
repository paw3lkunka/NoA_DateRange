namespace NoA.DateRange.Services;

/// <summary>
/// Service to privide date range string formatting utilities
/// </summary>
public interface IDateRangeService {
  /// <summary>
  /// <p>
  ///   Creates shorthand string representation of date range using the current thread culture.<br/>
  ///   Shortening works as follows:
  /// </p>
  /// <ul>
  ///   <li>e.g. <b>dd - dd.MM.yyyy</b> - when year and month is shared.</li>
  ///   <li>e.g. <b>dd.MM - dd.MM.yyyy</b> - when year is shared.</li>
  ///   <li>e.g. <b>dd.MM.yyyy - dd.MM.yyyy</b> - in all other cases.</li>
  /// </ul>
  /// </summary>
  /// <param name="startDate">Start date of date range</param>
  /// <param name="endDate">End date of date range</param>
  /// <returns>String that represents date range</returns>
  string CreateString(DateOnly startDate, DateOnly endDate);

  /// <inheritdoc cref="CreateString(System.DateOnly,System.DateOnly)"/>
  string CreateString(string startDate, string endDate);
}