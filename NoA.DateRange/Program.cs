using NoA.DateRange.Services;

if (args.Length is not 2) {
  Console.WriteLine("Program requires two arguments of type date e.g. 2000-04-07, 10/30/2008, 02.03.11");
  return 1;
}

// Decided not to use dependency injection, because it does not make sense right now
IDateRangeService dateRangeService = new DateRangeService();
string output;

try {
  output = dateRangeService.CreateString(args[0], args[1]);
}
catch (ArgumentNullException) {
  Console.WriteLine("Something terrible happend while executing program.");
  return 4;
}
catch (ArgumentException ex) {
  Console.WriteLine(ex.Message);
  return 2;
}
catch (FormatException) {
  Console.WriteLine("Could not parse one of the dates. Check dates formatting.");
  return 3;
}

Console.WriteLine(output);
return 0;