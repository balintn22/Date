using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Dates
{
    /// <summary>
    /// Represents a calendar date without time.
    /// </summary>
    [JsonConverter(typeof(DateJsonConverter))]
    [TypeConverter(typeof(DateTypeConverter))]  // Use DateTypeConverter to convert values to Date.
    public struct Date : IComparable<Date>, IComparable<DateTime>
    {
        [JsonIgnore]
        public int Year;

        [JsonIgnore]
        public int Month;

        [JsonIgnore]
        public int Day;

        public Date(int years, int months, int days)
        {
            // TODO: Implement validation

            Year = years;
            Month = months;
            Day = days;
        }

        /// <summary>A subset of DateTime format string that are acceptable for Dates.</summary>
        private static string[] standardDateFormatSpecifiers = { "d", "D", "m", "Y" };

        public string ToString(string format, IFormatProvider provider = null)
        {
            if (format.Length == 1)
            {
                Guard.AgainstUnsupportedValues(
                    format, nameof(format), standardDateFormatSpecifiers,
                    $"Unsupported format specifier. Must be one of {string.Join(", ", standardDateFormatSpecifiers)}");
            }

            return new DateTime(Year, Month, Day).ToString(format, provider);
        }

        /// <summary>
        /// Converts the date to a string using the ISO standard 2018-12-31 format.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0:0000}-{1:00}-{2:00}", Year, Month, Day);
        }

        public static Date Parse(string source)
        {
            return DateTime.Parse(source).ToDate();
        }

        public static bool TryParse(string source, out Date result)
        {
            DateTime dt;
            bool success = DateTime.TryParse(source, out dt);
            if (success)
                result = dt.ToDate();
            else
                result = new Date(0, 0, 0);
            return success;
        }


        #region Empty Representation

        public static Date Empty = new Date(0, 0, 0);

        public bool IsEmpty
        {
            get { return (Year == 0) && (Month == 0) && (Day == 0); }
        }

        #endregion Empty Representation


        #region Equality Checks

        public override bool Equals(object other)
        {
            if (Object.ReferenceEquals(null, other))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            if (other.GetType() != this.GetType())
                return false;

            return IsEqual((Date)other);
        }

        public bool Equals(Date other)
        {
            if (Object.ReferenceEquals(null, other))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            return IsEqual(other);
        }

        private bool IsEqual(Date other)
        {
            return (Year == other.Year)
                && (Month == other.Month)
                && (Day == other.Day);
        }

        public static bool operator ==(Date d1, Date d2)
        {
            return d1.Equals(d2);
        }

        public static bool operator !=(Date d1, Date d2)
        {
            return !d1.Equals(d2);
        }

        public override int GetHashCode()
        {
            // Note: This is not the most performant implementation, but will do for now.
            return ToString().GetHashCode();
        }

        public static bool operator <(Date d1, Date d2)
        {
            return d1.CompareTo(d2) == -1;
        }

        public static bool operator >(Date d1, Date d2)
        {
            return d1.CompareTo(d2) == 1;
        }

        public static bool operator <=(Date d1, Date d2)
        {
            return d1.CompareTo(d2) != 1;
        }

        public static bool operator >=(Date d1, Date d2)
        {
            return d1.CompareTo(d2) != -1;
        }

        #endregion Equality Checks


        #region Implement IComparable<Date>, IComparable<DateTime>

        // Return a this - other order sign value
        public int CompareTo(Date other)
        {
            if (Year > other.Year) return 1;
            if (Year < other.Year) return -1;
            if (Month > other.Month) return 1;
            if (Month < other.Month) return -1;
            if (Day > other.Day) return 1;
            if (Day < other.Day) return -1;
            return 0;
        }

        public int CompareTo(DateTime other)
        {
            if (Year > other.Year) return 1;
            if (Year < other.Year) return -1;
            if (Month > other.Month) return 1;
            if (Month < other.Month) return -1;
            if (Day > other.Day) return 1;
            if (Day < other.Day) return -1;
            if (0 < other.Hour) return -1;
            if (0 < other.Minute) return -1;
            if (0 < other.Second) return -1;
            if (0 < other.Millisecond) return -1;
            return 0;
        }

        #endregion Implement IComparable<Date>, IComparable<DateTime>


        #region Conversions

        public static implicit operator DateTime(Date date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static explicit operator Date(DateTime datetime)
        {
            return new Date(datetime.Year, datetime.Month, datetime.Day);
        }

        #endregion Conversions


        #region Operations with TimeSpans

        public static DateTime operator +(Date date, TimeSpan span)
        {
            return (DateTime)date + span;
        }

        public static DateTime operator -(Date date, TimeSpan span)
        {
            return (DateTime)date - span;
        }

        #endregion Operations with TimeSpans


        /// <summary>
        /// Checks if this date is within an interval.
        /// By default, includes boundaries.
        /// Boundaries are not required to be ordered.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="includeLowerLimit">Set to false to exclude the lower limit (that can be either d1 or d2).</param>
        /// <param name="includeHigherLimit">Set to false to exclude the higher limit (that can be either d1 or d2).</param>
        /// <returns></returns>
        public bool IsBetween(Date d1, Date d2, bool includeLowerLimit = true, bool includeHigherLimit = true)
        {
            Date lowerLimit, higherLimit;

            if (d1 <= d2)
            {   // Interval boundaries are in natural order
                lowerLimit = d1;
                higherLimit = d2;
            }
            else
            {   // Interval bundaries are in reverse order
                lowerLimit = d2;
                higherLimit = d1;
            }

            bool satisfiesLowerLimit = includeLowerLimit ? (lowerLimit <= this) : (lowerLimit < this);
            bool satisfiesHigherLimit = includeHigherLimit ? (this <= higherLimit) : (this < higherLimit);
            return satisfiesLowerLimit && satisfiesHigherLimit;
        }

        public static Date Now
        {
            get
            {
                DateTime now = DateTime.Now;
                return new Date(now.Year, now.Month, now.Day);
            }
        }

        public static Date UtcNow
        {
            get
            {
                DateTime now = DateTime.UtcNow;
                return new Date(now.Year, now.Month, now.Day);
            }
        }

        public static Date Today { get { return Date.Now; } }

        public static Date UtcToday { get { return Date.UtcNow; } }

        public static Date Yesterday
        {
            get
            {
                DateTime dt = DateTime.Now - TimeSpan.FromDays(1);
                return new Date(dt.Year, dt.Month, dt.Day);
            }
        }

        public static Date UtcYesterday
        {
            get
            {
                DateTime dt = DateTime.UtcNow - TimeSpan.FromDays(1);
                return new Date(dt.Year, dt.Month, dt.Day);
            }
        }

        public static Date Tomorrow
        {
            get
            {
                DateTime dt = DateTime.Now + TimeSpan.FromDays(1);
                return new Date(dt.Year, dt.Month, dt.Day);
            }
        }

        public static Date UtcTomorrow
        {
            get
            {
                DateTime dt = DateTime.UtcNow + TimeSpan.FromDays(1);
                return new Date(dt.Year, dt.Month, dt.Day);
            }
        }

        public static Date MinValue
        {
            get { return DateTime.MinValue.ToDate(); }
        }

        public static Date MaxValue
        {
            get { return DateTime.MaxValue.ToDate(); }
        }
    }

    public static class DateTimeExtensions
    {
        public static Date ToDate(this DateTime self)
        {
            return new Date(self.Year, self.Month, self.Day);
        }
    }
}
