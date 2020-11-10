using System;

namespace Dates
{
    /// <summary>
    /// Represents a calendar interval, that can be used in operations with dates.
    /// </summary>
    public class DateSpan : IComparable<DateSpan>, IComparable<TimeSpan>
    {
        public double Days { get; set; }

        public DateSpan(double days)
        {
            Days = days;
        }

        public static DateSpan FromDays(double days)
        {
            return new DateSpan(days);
        }

        public static DateSpan FromWeeks(double days)
        {
            return new DateSpan(days * 7);
        }


        #region Equality Checks between DateSpans

        public override bool Equals(object other)
        {
            if (Object.ReferenceEquals(null, other))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            if (other.GetType() != this.GetType())
                return false;

            return IsEqual((DateSpan)other);
        }

        public bool Equals(DateSpan other)
        {
            if (Object.ReferenceEquals(null, other))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            return IsEqual(other);
        }

        private bool IsEqual(DateSpan other)
        {
            return Days == other.Days;
        }

        public static bool operator ==(DateSpan d1, DateSpan d2)
        {
            return d1.Equals(d2);
        }

        public static bool operator !=(DateSpan d1, DateSpan d2)
        {
            return !d1.Equals(d2);
        }

        public override int GetHashCode()
        {
            return Days.GetHashCode();
        }

        public static bool operator <(DateSpan d1, DateSpan d2)
        {
            return d1.CompareTo(d2) == -1;
        }

        public static bool operator >(DateSpan d1, DateSpan d2)
        {
            return d1.CompareTo(d2) == 1;
        }

        public static bool operator <=(DateSpan d1, DateSpan d2)
        {
            return d1.CompareTo(d2) != 1;
        }

        public static bool operator >=(DateSpan d1, DateSpan d2)
        {
            return d1.CompareTo(d2) != -1;
        }

        #endregion Equality Checks between DateSpans


        #region Implement IComparable<DateSpan>, IComparable<TimeSpan>

        // Return a this - other order sign value
        public int CompareTo(DateSpan other)
        {
            return Days.CompareTo(other.Days);
        }

        public int CompareTo(TimeSpan other)
        {
            return TimeSpan.FromDays(Days).CompareTo(other);
        }

        #endregion Implement IComparable<DateSpan>, IComparable<TimeSpan>


        #region Conversions

        public static implicit operator TimeSpan(DateSpan dateSpan)
        {
            return TimeSpan.FromDays(dateSpan.Days);
        }

        public static explicit operator DateSpan(TimeSpan timeSpan)
        {
            return new DateSpan(timeSpan.Days);
        }

        #endregion Conversions


        #region Operations with Dates and DateSpans

        public static Date operator +(Date date, DateSpan span)
        {
            return (Date)((DateTime)date + span);
        }

        public static Date operator -(Date date, DateSpan span)
        {
            return (Date)((DateTime)date - span);
        }

        #endregion Operations with Dates and DateSpans


        #region Operations with  DateSpans

        public static DateSpan operator +(DateSpan ds1, DateSpan ds2)
        {
            return new DateSpan(ds1.Days + ds2.Days);
        }

        public static DateSpan operator -(DateSpan ds1, DateSpan ds2)
        {
            return new DateSpan(ds1.Days - ds2.Days);
        }

        #endregion Operations with DateSpans
    }
}
