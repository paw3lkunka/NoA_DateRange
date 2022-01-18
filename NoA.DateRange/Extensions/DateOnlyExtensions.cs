using System.Linq.Expressions;

namespace NoA.DateRange.Extensions; 

/// <summary>
/// Extension methods for DateOnly struct
/// </summary>
public static class DateOnlyExtensions {
  /// <summary>
  /// <p>Converts a string that contains string representation of a date to its DateOnly equivalent by following steps:</p>
  /// <ul>
  ///   <li>trying to use the conventions of the current culture</li>
  ///   <li>trying to use all available CultureInfo of type CultureTypes.SpecificCultures </li>
  /// </ul>
  /// </summary>
  /// <param name="dateOnly">DateOnly entity</param>
  /// <param name="dateString">Date expressed as string</param>
  /// <returns>DateOnly struct containing date expressed in dateString</returns>
  /// <exception cref="ArgumentNullException">dateString is null</exception>
  /// <exception cref="FormatException">dateString does not contain a valid string representation of a date</exception>
  public static DateOnly MultiCultureParse(this DateOnly dateOnly, string dateString) {
    if (dateString is null) {
      throw new ArgumentNullException($"{nameof(dateString)} is null");
    }
    // Try current culture
    if (DateOnly.TryParse(dateString, out dateOnly)) {
      return dateOnly;
    }
    // Try all other specific cultures
    CultureInfo[] specificCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
    foreach (CultureInfo culture in specificCultures) {
      if (DateOnly.TryParse(dateString, culture, DateTimeStyles.None, out dateOnly)) {
        return dateOnly;
      }
    }
    // If all specific cultures fail
    throw new FormatException($"{nameof(dateOnly)} does not contain a valid string representation of a date.");
  }
}