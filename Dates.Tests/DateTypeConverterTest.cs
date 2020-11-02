using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace Dates.Tests
{
    [TestClass]
    public class DateTypeConverterTest
    {
        #region CanConvertFrom tests

        [TestMethod]
        public void DateTypeConverter_CanConvertFromTest_NullSourceType()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Date));

            Assert.IsTrue(converter.CanConvertFrom(typeof(string)));

            Assert.IsFalse(converter.CanConvertFrom(typeof(int)));
            Assert.IsFalse(converter.CanConvertFrom(typeof(long)));
            Assert.IsFalse(converter.CanConvertFrom(typeof(DateTime)));
        }

        #endregion CanConvertFrom tests


        #region ConvertFrom tests

        [TestMethod]
        public void DateTypeConverter_ConvertFromTest_DottedSource()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Date));
            Date result = (Date)converter.ConvertFrom("2019.03.13");

            Assert.AreEqual(2019, result.Year);
            Assert.AreEqual(3, result.Month);
            Assert.AreEqual(13, result.Day);
        }

        [TestMethod]
        public void DateTypeConverter_ConvertFromTest_IsoSource()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Date));
            Date result = (Date)converter.ConvertFrom("2019-03-13");

            Assert.AreEqual(2019, result.Year);
            Assert.AreEqual(3, result.Month);
            Assert.AreEqual(13, result.Day);
        }

        [TestMethod]
        public void DateTypeConverter_ConvertFromTest_NullSource()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Date));
            Date result = (Date)converter.ConvertFrom(null);

            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        public void DateTypeConverter_ConvertFromTest_EmptySource()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Date));
            Date result = (Date)converter.ConvertFrom("");

            Assert.IsTrue(result.IsEmpty);
        }

        #endregion ConvertFrom tests
    }
}
