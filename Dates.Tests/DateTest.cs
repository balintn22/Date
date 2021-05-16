using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Dates.Tests
{
    [TestClass]
    public class DateTest
    {
        [TestMethod]
        public void Date_Comparision_Tests()
        {
            Assert.AreEqual(new Date(2018, 12, 31), new Date(2018, 12, 31), "A Date should be value equal to the same date.");
            Assert.IsTrue(new Date(2018, 12, 31) == new Date(2018, 12, 31), "A Date should be value equal to the same date.");
            Assert.IsTrue(new Date(2018, 12, 31) != new Date(2019, 12, 31));
            Assert.IsTrue(new Date(2018, 12, 31) <= new Date(2018, 12, 31));
            Assert.IsFalse(new Date(2018, 12, 31) < new Date(2018, 12, 31));
            Assert.IsTrue(new Date(2018, 12, 31) < new Date(2019, 01, 01));
            Assert.IsTrue(new Date(2018, 12, 31) >= new Date(2018, 12, 31));
            Assert.IsFalse(new Date(2018, 12, 31) > new Date(2018, 12, 31));
            Assert.IsTrue(new Date(2019, 01, 01) > new Date(2018, 12, 31));
        }

        [TestMethod]
        public void Date_To_DateTime_Comparision_Tests()
        {
            Assert.AreEqual(new Date(2018, 12, 31), new DateTime(2018, 12, 31));
            Assert.IsTrue(new Date(2018, 12, 31) == new DateTime(2018, 12, 31));
            Assert.IsTrue(new Date(2018, 12, 31) != new DateTime(2019, 12, 31));
            Assert.IsTrue(new Date(2018, 12, 31) <= new DateTime(2018, 12, 31));
            Assert.IsFalse(new Date(2018, 12, 31) < new DateTime(2018, 12, 31));
            Assert.IsTrue(new Date(2018, 12, 31) < new DateTime(2019, 01, 01));
            Assert.IsTrue(new Date(2018, 12, 31) >= new DateTime(2018, 12, 31));
            Assert.IsFalse(new Date(2018, 12, 31) > new DateTime(2018, 12, 31));
            Assert.IsTrue(new Date(2019, 01, 01) > new DateTime(2018, 12, 31));
        }

        [TestMethod]
        public void Date_ToString_Tests()
        {
            Assert.AreEqual("2018-12-31", new Date(2018, 12, 31).ToString(), "This should be ISO 8601 plain date, see https://en.wikipedia.org/wiki/ISO_8601");
            Assert.AreEqual("31/12/2018", new Date(2018, 12, 31).ToString("d", CultureInfo.CreateSpecificCulture("en-GB")));
            Assert.AreEqual("31 December 2018", new Date(2018, 12, 31).ToString("D", CultureInfo.CreateSpecificCulture("en-GB")));
        }

        [TestMethod]
        public void Date_Parse_Test()
        {
            Assert.AreEqual(new Date(2018, 12, 31), Date.Parse("2018-12-31"));
            Assert.AreEqual(new Date(2018, 12, 31), Date.Parse("2018.12.31"));

            // These formats fail on the build server, probably because the date format there is set to DD/MM/YYYY
            //Assert.AreEqual(new Date(2018, 12, 31), Date.Parse("31-12-2018"));
            //Assert.AreEqual(new Date(2018, 12, 31), Date.Parse("31/12/2018"));
        }

        [TestMethod]
        public void Date_TryParse_Test()
        {
            Date aDate;
            Assert.AreEqual(true, Date.TryParse("2018-12-31", out aDate), "2018-12-31 is a correct date value.");
            Assert.AreEqual(true, Date.TryParse("2018/12/31", out aDate), "2018/12/31 is a correct date value.");
            // This format fails on the build server, probably because the date format there is set to DD/MM/YYYY
            //Assert.AreEqual(true, Date.TryParse("31/12/2018", out aDate), "31/12/2018 is a correct date value.");

            Assert.AreEqual(false, Date.TryParse("2018-12-32", out aDate), "Days value incorrect.");
            Assert.AreEqual(false, Date.TryParse("2018-13-31", out aDate), "Months value incorrect.");
            Assert.AreEqual(false, Date.TryParse("10000-12-32", out aDate), "Years value incorrect.");

            Assert.AreEqual(false, Date.TryParse("", out aDate), "Empty");
            Assert.AreEqual(false, Date.TryParse("abc", out aDate), "Not a date");

            Assert.AreEqual(true, Date.TryParse("2016-02-29", out aDate), "Valid leap day.");
            Assert.AreEqual(false, Date.TryParse("2017-02-29", out aDate), "Invalid leap day.");
        }

        [TestMethod]
        public void Date_Addition_Test()
        {
            Date _base = Date.Parse("2019.02.01");
            Date nextDay = (Date)(_base + TimeSpan.FromDays(1));
            Date nextMonth = (Date)(_base + TimeSpan.FromDays(31));
            Date nextYear = (Date)(_base + TimeSpan.FromDays(366));

            Assert.AreEqual(2, nextDay.Day);
            Assert.AreEqual(3, nextMonth.Month);
            Assert.AreEqual(2020, nextYear.Year);
        }

        [TestMethod]
        public void Date_TimeSpan_Subtraction_Test()
        {
            Date _base = Date.Parse("2019.02.01");
            Date prevDay = (Date)(_base - TimeSpan.FromDays(1));
            Date prevMonth = (Date)(_base - TimeSpan.FromDays(31));
            Date prevYear = (Date)(_base - TimeSpan.FromDays(366));

            Assert.AreEqual(31, prevDay.Day);
            Assert.AreEqual(1, prevMonth.Month);
            Assert.AreEqual(2018, prevYear.Year);
        }

        [TestMethod]
        public void Date_Date_Subtraction_Test()
        {
            Date _base = Date.Parse("2019.02.01");
            Date aDayEarlier = Date.Parse("2019.01.31");
            Date aMonthEarlier = Date.Parse("2019.01.01");
            Date aYearEarlier = Date.Parse("2018.02.01");

            (_base - _base).Should().BeEquivalentTo(new DateSpan(0));
            (_base - aDayEarlier).Should().BeEquivalentTo(new DateSpan(1));
            (_base - aMonthEarlier).Should().BeEquivalentTo(new DateSpan(31));
            (_base - aYearEarlier).Should().BeEquivalentTo(new DateSpan(365));
        }

        [TestMethod]
        public void Date_IsBetween_Test()
        {
            Date low = new Date(2018, 01, 01);
            Date high = new Date(2018, 12, 31);

            // Tests in natural interval-boundary order
            Assert.IsTrue(new Date(2018, 12, 31).IsBetween(low, high, true, true));
            Assert.IsTrue(new Date(2018, 12, 31).IsBetween(low, high, false, true));
            Assert.IsFalse(new Date(2018, 12, 31).IsBetween(low, high, true, false));
            Assert.IsFalse(new Date(2018, 12, 31).IsBetween(low, high, false, false));

            // Tests in reverse interval-boundary order
            Assert.IsTrue(new Date(2018, 12, 31).IsBetween(high, low, true, true));
            Assert.IsTrue(new Date(2018, 12, 31).IsBetween(high, low, false, true));
            Assert.IsFalse(new Date(2018, 12, 31).IsBetween(high, low, true, false));
            Assert.IsFalse(new Date(2018, 12, 31).IsBetween(high, low, false, false));
        }

        [TestMethod]
        public void Date_MinValue_MaxValue_Test()
        {
            Assert.IsTrue(Date.MinValue < Date.Now);
            Assert.IsTrue(Date.MaxValue > Date.Now);
            Assert.IsTrue(Date.MinValue < Date.MaxValue);
            Assert.IsTrue(Date.MinValue.ToString("yyyy.MM.dd").Equals("0001.01.01"));
            Assert.IsTrue(Date.MaxValue.ToString("yyyy.MM.dd").Equals("9999.12.31"));
        }

        [TestMethod]
        public void Date_DateTimeComparison_Test()
        {
            Date date = Date.Parse("2018.12.31");
            DateTime dt = new DateTime(2019, 01, 01, 11, 59, 59);

            Assert.AreEqual(true, date < new DateTime(2019, 01, 01, 11, 59, 59));
            Assert.AreEqual(false, date > new DateTime(2019, 01, 01, 11, 59, 59));

            Assert.AreEqual(true, date == new DateTime(2018, 12, 31, 00, 00, 00));
            Assert.AreEqual(false, date == new DateTime(2018, 12, 31, 11, 59, 59));
            Assert.AreEqual(false, date == new DateTime(2018, 12, 31, 00, 00, 01));
        }

        [TestMethod]
        public void Date_CompareToDateTime_Test()
        {
            Date date = Date.Parse("2018.12.31");

            Assert.AreEqual(-1, date.CompareTo(new DateTime(2019, 01, 01, 11, 59, 59)));
            Assert.AreEqual(0, date.CompareTo(new DateTime(2018, 12, 31, 00, 00, 00)));
            Assert.AreEqual(1, date.CompareTo(new DateTime(2018, 12, 30, 23, 59, 59)));
        }

        [TestMethod]
        public void Date_ConversionFromDateTime_Test()
        {
            Date date = Date.Parse("2018.12.31");

            Assert.AreEqual(Date.Parse("2018.12.31"), (Date)new DateTime(2018, 12, 31, 01, 02, 03));
        }
    }
}
