using FluentAssertions;
using System;

namespace Dates.Tests;

[TestClass()]
public class TimeSpanHelperTests
{
    [TestMethod()]
    public void TryParseEx_HappyCases()
    {
        TimeSpanHelper.TryParseEx("01.234", out TimeSpan value1).Should().BeTrue();
        value1.Should().Be(new TimeSpan(00, 00, 00, 01, 234));

        TimeSpanHelper.TryParseEx("01:02.345", out TimeSpan value2).Should().BeTrue();
        value2.Should().Be(new TimeSpan(00, 00, 01, 02, 345));

        TimeSpanHelper.TryParseEx("01:02:03.456", out TimeSpan value3).Should().BeTrue();
        value3.Should().Be(new TimeSpan(00, 01, 02, 03, 456));
    }
}