using Newtonsoft.Json;
using System;

namespace Dates
{
    public class DateJsonConverter : JsonConverter<Date?>
    {
        public override void WriteJson(JsonWriter writer, Date? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value.ToString());
            else
                writer.WriteNull();
        }

        public override Date? ReadJson(JsonReader reader, Type objectType, Date? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return !string.IsNullOrEmpty(s) ? Date.Parse(s) : new Date?();
        }
    }
}
