# Date

v1.2.0: Added TimeSpanHelper.TryParseEx() and TimeSpanHelper.ParseEx() to support new time formats.

v1.1.0: Added support for Net6, Net8, Net9

v1.0.8: Added Date.FromDateTime(DateTime dt) conversion.

Implements Date support without the need to neglect the time part.
- Conversion to ISO and other formatted date strings
- Parsing
- Conversions to/from DateTime
- Comparisons between Date values as well as Date and DateTime values.
- Addition and subscription with TimeSpan values.

## Examples
    Console.WriteLine($"Date is {Date.Now}");
    Console.WriteLine($"Date is {DateTime.Now.ToDate()}");
    Console.WriteLine($"Minimum Date is {DateTime.MinValue.ToDate()}");
    Console.WriteLine($"Maximum Date is {DateTime.MaxValue.ToDate()}");
    Console.WriteLine($"Today + 1 hour 59 minutes is {Date.Now + new TimeSpan(1, 59, 0)}");
    Console.WriteLine($"Current time cast to Date is {(Date)DateTime.Now}");
    Console.WriteLine($"Current date cast to DateTime is {(DateTime)Date.Now}");
    Console.WriteLine($"Current date cast to DateTime is {DateTime.FromDateTime(Date.Now)}");
    
    // Comparisions between two Dates as well as between a Date and a DateTime are supported
    Date newYearsEve = new Date(2020, 12, 31);
    Date christmas = new Date(2020, 12, 24);
    string beforeOrAfter = christmas < newYearsEve ? "before" : "after";
    Console.WriteLine($"Christmas is {beforeOrAfter} New Years Eve");
    
    string dateString = "2020-12-31";
    Console.WriteLine($"Parsing the date {dateString} gives {Date.Parse(dateString)}");
    
    // Additions and subtractions are supported via TimeSpans
    var fourDays = new TimeSpan(4, 0, 0, 0);
    Console.WriteLine($"Four days before Christmas is {(Date)(christmas - fourDays)}");

## Framework Support
- .Net Framework 4.0+
- .Net Core 6.0+
- .Net Standard 1.0+

## Installation
Using Visual Studio Nuget Package Manager (UI):
- Search for "Date Balint"
- Install the package in your project

Using Visual Studio Package Manager Console:

    Install-Package Date -project MyProject
