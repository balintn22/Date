namespace Dates
{
    /// <summary>
    /// Represents a daet interval with support for testing inclusion.
    /// </summary>
    public class DateInterval
    {
        public Date From { get; set; }

        public Date To { get; set; }


        public bool Includes(Date date, bool includeFrom = true, bool includeTo = true)
        {
            return date.IsBetween(From, To, includeLowerLimit: includeFrom, includeHigherLimit: includeTo);
        }
    }
}
