using Dates;
using System;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Wr($"Date is {Date.Now}");
            Wr($"Date is {DateTime.Now.ToDate()}");
            Wr($"Minimum Date is {DateTime.MinValue.ToDate()}");
            Wr($"Maximum Date is {DateTime.MaxValue.ToDate()}");
            Wr($"Today + 1 hour 59 minutes is {Date.Now + new TimeSpan(1, 59, 0)}");
            Wr($"Current time cast to Date is {(Date)DateTime.Now}");
            Wr($"Current date cast to DateTime is {(DateTime)Date.Now}");

            Date newYearsEve = new Date(2020, 12, 31);
            Date christmas = new Date(2020, 12, 24);
            string beforeOrAfter = christmas < newYearsEve ? "before" : "after";
            Wr($"Christmas is {beforeOrAfter} New Years Eve");

            string dateString = "2020-12-31";
            Wr($"Parsing the date {dateString} gives {Date.Parse(dateString)}");
            Console.ReadLine();
        }

        private static void Wr(string str)
        {
            Console.WriteLine(str);
        }
    }
}
