using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Converters
{
    /// <summary>
    /// Час типу Local, або Unspecified - в прозоро вважається українським
    /// </summary>
    public class DateTimeConverter : JsonConverter
    {
        static TimeZoneInfo _timeZone;
        static DateTimeConverter()
        {
            try
            {
                _timeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            }
            catch
            {
                _timeZone = TimeZoneInfo.FromSerializedString("FLE Standard Time;120;(UTC+02:00) Вільнюс, Гельсінкі, Київ, Рига, Софія, Таллінн;Фінляндія (зима);Фінляндія (літо);[01:01:0001;12:31:9999;60;[0;03:00:00;3;5;0;];[0;04:00:00;10;5;0;];];");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DateTime) == objectType || typeof(DateTime?) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return typeof(DateTime) == objectType ? DateTime.MinValue.ToUniversalTime() : (DateTime?) null;
                case JsonToken.Date:
                    DateTime? dt = reader.Value as DateTime?;
                    if (dt.HasValue)
                    {
                        if (dt.Value.Kind == DateTimeKind.Unspecified)
                        {
                            Trace.TraceWarning("Received ambiguous DateTime {0}. It will be treated as LFE, and converted to utc. {1}", dt.Value.Kind);
                            dt = TimeZoneInfo.ConvertTimeToUtc(dt.Value, _timeZone);
                        }
                        else
                        {
                            dt = dt.Value.ToUniversalTime();
                        }
                    }
                    return dt;
                default:
                    throw new ArgumentException("Invalid token type");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(ToLfeString((DateTime) value));
        }

        public static string ToLfeString(DateTime dateTime)
        {
            DateTime lfe;
            switch (dateTime.Kind)
            {
                case DateTimeKind.Local:
                    Trace.TraceWarning("Sending local DateTime. It will be converted to utc, and then to LFE. " + new StackTrace(1));
                    lfe = TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), _timeZone);
                    break;
                default:
                    lfe = TimeZoneInfo.ConvertTimeFromUtc(dateTime, _timeZone);
                    break;
            }
            TimeSpan offset = _timeZone.GetUtcOffset(lfe);
            return lfe.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                + (lfe.Ticks % TimeSpan.TicksPerSecond > 0 ? lfe.ToString("'.'ffffff") : "")
                + (offset < TimeSpan.Zero ? '-' : '+')
                + offset.ToString("hh':'mm");
        }
    }
}
