using System;

namespace Dates
{
    public static class TimeSpanHelper
    {
        /// <summary>
        /// Parses a time string into a TimeSpan.
        /// Accepts formats not supported by TimeSpan.Parse, like
        ///  "01.345"      (time without hours and minutes)
        ///  "01:12.345"   (time without hours)
        /// </summary>
        /// <param name="source">Source string to parse in a format like "01:02:03.456",
        /// "01:02.345" or "01:234"</param>
        /// <returns>The parse TimeSpan.</returns>
        /// <exception cref="ArgumentNullException">If the source string is null</exception>
        /// <exception cref="ArgumentException">If TimeSpan parsing fails.</exception>
        public static TimeSpan ParseEx(string source)
        {
            if(source == null)
                throw new ArgumentNullException("source");

            if(!TryParseEx(source, out TimeSpan value))
                throw new ArgumentException($"Couldn't parse {source} as a TimeSpan.");

            return value;
        }

        /// <summary>
        /// Parses a time string into a TimeSpan.
        /// Accepts formats not supported by TimeSpan.Parse, like
        ///  "01.345"      (time without hours and minutes)
        ///  "01:12.345"   (time without hours)
        /// </summary>
        /// <param name="source">Source string to parse in a format like "01:02:03.456",
        /// "01:02.345" or "01:234"</param>
        /// <param name="value">The resultant TimeSpan if parsing is successful.</param>
        /// <returns>Success</returns>
        public static bool TryParseEx(string source, out TimeSpan value)
        {
            value = TimeSpan.Zero;

            if (source == null)
                return false;

            var tokens = source.Split(':');

            switch (tokens.Length)
            {
                case 0: value = TimeSpan.Zero; return true;
                case 1: value = TimeSpan.Parse($"00:00:{source}"); return true;
                case 2: value = TimeSpan.Parse($"00:{source}"); return true;
                case 3: value = TimeSpan.Parse(source); return true;
                default: return false;
            }
        }
    }
}