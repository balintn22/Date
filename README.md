# Date

Implements Date support without the need to neglect the time part.
- Conversion to ISO and other formatted date strings
- Parsing
- Conversions to/from DateTime
- Comparisons between Date values as well as Date and DateTime values.

##Examples
    Console.WriteLine($"Date is {Date.Now}");
    Console.WriteLine($"Date is {DateTime.Now.ToDate()}");
    Console.WriteLine($"Minimum Date is {DateTime.MinValue.ToDate()}");
    Console.WriteLine($"Maximum Date is {DateTime.MaxValue.ToDate()}");
    Console.WriteLine($"Today + 1 hour 59 minutes is {Date.Now + new TimeSpan(1, 59, 0)}");
    Console.WriteLine($"Current time cast to Date is {(Date)DateTime.Now}");
    Console.WriteLine($"Current date cast to DateTime is {(DateTime)Date.Now}");

    Date newYearsEve = new Date(2020, 12, 31);
    Date christmas = new Date(2020, 12, 24);
    string beforeOrAfter = christmas < newYearsEve ? "before" : "after";
    Console.WriteLine($"Christmas is {beforeOrAfter} New Years Eve");

    string dateString = "2020-12-31";
    Console.WriteLine($"Parsing the date {dateString} gives {Date.Parse(dateString)}");
    Console.ReadLine();
