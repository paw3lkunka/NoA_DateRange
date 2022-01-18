using NoA.DateRange.Enums;

namespace NoA.DateRange.Services;

/// <inheritdoc/>
public class DateRangeService : IDateRangeService {
  /// <inheritdoc/>
  /// <exception cref="ArgumentException">startDate is greater than or equal endDate</exception>
  public string CreateString(DateOnly startDate, DateOnly endDate) {
    // Check if arguments are valid
    if (startDate >= endDate) {
      throw new ArgumentException("Start date must be an earlier date than end date.");
    }
    // Get date format for current culture
    string dateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
    // Get date format components volume e.g. "dd" & "d", "yyyy" & "yy" etc.
    string[] ymd = dateFormat.Split(CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);
    // Get date format components order
    DateFormatOrder dateFormatOrder;
    if (dateFormat.StartsWith('y')) {
      dateFormatOrder = DateFormatOrder.BigEndian;
    }
    else if (dateFormat.StartsWith('d')) {
      dateFormatOrder = DateFormatOrder.LittleEndian;
    }
    else {
      dateFormatOrder = DateFormatOrder.MiddleEndian;
    }
    // Check if years and/or months are equal
    if (startDate.Year == endDate.Year) {
      if (startDate.Month == endDate.Month) {
        return dateFormatOrder switch {
          DateFormatOrder.LittleEndian =>
            $"{startDate.ToString(ymd[0])} - {endDate.ToString($"{ymd[0]}/{ymd[1]}/{ymd[2]}")}",
          DateFormatOrder.MiddleEndian =>
            $"{startDate.ToString($"{ymd[0]}/{ymd[1]}")} - {endDate.ToString($"{ymd[1]}/{ymd[2]}")}",
          DateFormatOrder.BigEndian or _ =>
            $"{startDate.ToString($"{ymd[0]}/{ymd[1]}/{ymd[2]}")} - {endDate.ToString(ymd[2])}",
        };
      }
      else {
        return dateFormatOrder switch {
          DateFormatOrder.LittleEndian or DateFormatOrder.MiddleEndian =>
            $"{startDate.ToString($"{ymd[0]}/{ymd[1]}")} - {endDate.ToString($"{ymd[0]}/{ymd[1]}/{ymd[2]}")}",
          DateFormatOrder.BigEndian or _ =>
            $"{startDate.ToString($"{ymd[0]}/{ymd[1]}/{ymd[2]}")} - {endDate.ToString($"{ymd[1]}/{ymd[2]}")}",
        };
      }
    }
    else {
      return $"{startDate.ToString(dateFormat)} - {endDate.ToString(dateFormat)}";
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