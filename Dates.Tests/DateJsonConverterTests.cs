using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Dates.Tests
{
    [TestClass]
    public class DateJsonConverterTests
    {
        [TestMethod]
        public void Date_Serialize_Test()
        {
            DateJsonTest test = new DateJsonTest() { Date = new Date(2018, 12, 31), NullableDate = new Date(2019, 01, 01) };
            string serialized = JsonConvert.SerializeObject(test);

            string expected = "{\"Date\":\"2018-12-31\",\"NullableDate\":\"2019-01-01\"}";
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void Date_Serialize_Test_With_Null()
        {
            DateJsonTest test = new DateJsonTest() { Date = new Date(2018, 12, 31) };
            string serialized = JsonConvert.SerializeObject(test);

            string expected = "{\"Date\":\"2018-12-31\",\"NullableDate\":null}";
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void Date_Deserialize_Test()
        {
            string json = "{ \"Date\":\"2018-12-31\", \"NullableDate\": \"2019-01-01\" }";
            DateJsonTest deserialized = JsonConvert.DeserializeObject<DateJsonTest>(json);

            Assert.AreEqual(2019, deserialized.NullableDate.Value.Year);
            Assert.AreEqual(1, deserialized.NullableDate.Value.Month);
            Assert.AreEqual(1, deserialized.NullableDate.Value.Day);
            Assert.AreEqual(2018, deserialized.Date.Year);
            Assert.AreEqual(12, deserialized.Date.Month);
            Assert.AreEqual(31, deserialized.Date.Day);
        }

        [TestMethod]
        public void Date_Deserialize_Test_With_Null()
        {
            string json = "{ \"Date\":\"2018-12-31\", \"NullableDate\": null }";
            DateJsonTest deserialized = JsonConvert.DeserializeObject<DateJsonTest>(json);

            Assert.IsNull(deserialized.NullableDate);
            Assert.AreEqual(2018, deserialized.Date.Year);
            Assert.AreEqual(12, deserialized.Date.Month);
            Assert.AreEqual(31, deserialized.Date.Day);
        }
    }

    public class DateJsonTest
    {
        public Date Date { get; set; }
        public Date? NullableDate { get; set; }
    }
}
