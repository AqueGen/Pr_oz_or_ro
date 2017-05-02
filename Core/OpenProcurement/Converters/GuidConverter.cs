using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Converters
{
    public class GuidConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Guid) == objectType || typeof(Guid?) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return typeof(Guid) == objectType ? Guid.Empty : (Guid?) null;
                case JsonToken.String:
                    string str = reader.Value as string;
                    if (string.IsNullOrEmpty(str))
                        return Guid.Empty;
                    else
                        try
                        {
                            return new Guid(str);
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceWarning("Guid '{0}' cannot be parsed. {1}", str, ex);
                            throw;
                        }
                default:
                    throw new ArgumentException("Invalid token type");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Guid) value).ToString("N"));
        }
    }
}
