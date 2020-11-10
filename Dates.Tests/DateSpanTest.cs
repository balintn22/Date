using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Dates.Tests
{
    [TestClass]
    public class DateSpanTest
    {
        [TestMethod]
        public void DateSPan_Comparision_Tests()
        {
            Assert.AreEqual(new DateSpan(1), new DateSpan(1));
            Assert.AreNotEqual(new DateSpan(1), new DateSpan(2));
        }

        [TestMethod]
        public void DateSpan_To_TimeSpan_Comparision_Tests()
        {
            Assert.AreEqual(new DateSpan(1), TimeSpan.FromDays(1));
            Assert.AreNotEqual(new DateSpan(1), TimeSpan.FromDays(2));
        }

        [TestMethod]
        public void DateSpan_Addition_Test()
        {
            Date _base = Date.Parse("2019.02.01");
            Date nextDay = _base + DateSpan.FromDays(1);

            Assert.AreEqual(2, nextDay.Day);
        }

        [TestMethod]
        public void DateSpan_Subtraction_Test()
        {
            Date _base = Date.Parse("2019.02.01");
            Date prevDay = _base - DateSpan.FromDays(1);

            Assert.AreEqual(31, prevDay.Day);
        }

        [TestMethod]
        public void DateSpan_TimeSpanComparison_Test()
        {
            Assert.AreEqual(new DateSpan(1), TimeSpan.FromDays(1));
            Assert.AreEqual(TimeSpan.FromDays(1), new DateSpan(1));

            Assert.AreNotEqual(new DateSpan(2), TimeSpan.FromDays(1));
            Assert.AreNotEqual(TimeSpan.FromDays(1), new DateSpan(2));
        }

        [TestMethod]
        public void DateSpan_ConversionFromTimeSPan_Test()
        {
            TimeSpan ts = TimeSpan.FromDays(1);
            DateSpan ds = (DateSpan)ts;

            ds.Days.Should().Be(1);
        }
    }
}
