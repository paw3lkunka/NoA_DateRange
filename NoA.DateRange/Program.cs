// See https://aka.ms/new-console-template for more information

using System.Globalization;


CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
Console.WriteLine(cultures.Length);
foreach (var cul in cultures) {
  DateTimeFormatInfo dtfi = cul.DateTimeFormat;
  Console.WriteLine(cul.Name);
  Console.WriteLine($"DateSeparator: {dtfi.DateSeparator}");
  Console.WriteLine($"short: {dtfi.ShortDatePattern}");
  Console.WriteLine($"long: {dtfi.LongDatePattern}");
  Console.WriteLine($"year-month: {dtfi.YearMonthPattern}");
  Console.WriteLine($"month-day: {dtfi.MonthDayPattern}");
  Console.WriteLine();
}

