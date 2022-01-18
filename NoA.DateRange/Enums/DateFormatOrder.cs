namespace NoA.DateRange.Enums;

/// <summary>
/// Enum expressing the order of date components (year, month, day) in its format
/// </summary>
public enum DateFormatOrder {
  /// <summary>
  /// Big-endian (year, month, day) e.g. yyyy-MM-dd
  /// </summary>
  BigEndian,
  /// <summary>
  /// Little-endian (day, month, year) e.g. dd/MM/yyyy
  /// </summary>
  LittleEndian,
  /// <summary>
  /// Middle-endian (month, day, year) e.g. MM/dd/yyyy
  /// </summary>
  MiddleEndian,
}