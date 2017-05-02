using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kapitalist.Core.OpenProcurement.Converters
{
    public class DecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(decimal) == objectType || typeof(decimal?) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return typeof(decimal) == objectType ? decimal.Zero : (decimal?) null;
                case JsonToken.Integer:
                case JsonToken.Float:
                    return JToken.Load(reader).ToObject<decimal>();
                case JsonToken.String:
                    string str = reader.Value as string;
                    if (string.IsNullOrEmpty(str))
                        goto case JsonToken.Null;
                    else {
                        decimal value;
                        if (decimal.TryParse(str, out value))
                            return value;
                        else
                        {
                            double valueDouble;
                            if (double.TryParse(str, out valueDouble))
                                return (decimal)valueDouble;
                            else
                                throw new ArgumentException("'" + str + "' cannot be converted to decimal");
                        }
                    }
                default:
                    throw new ArgumentException("Invalid token type " + reader.TokenType);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
