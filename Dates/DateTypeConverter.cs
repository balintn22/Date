using System;
using System.ComponentModel;
using System.Globalization;

namespace Dates
{
    public class DateTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return Date.Empty;

            var valueAsString = value as string;
            if (string.IsNullOrWhiteSpace(valueAsString))
                return Date.Empty;

            return Date.Parse(valueAsString);
        }
    }
}
