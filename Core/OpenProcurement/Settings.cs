using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kapitalist.Core.OpenProcurement.Converters;

namespace Kapitalist.Core.OpenProcurement
{
    public class Settings
    {
        public static readonly JsonSerializerSettings SerializerSettings;

        static Settings()
        {
            SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            SerializerSettings.Converters.Add(new GuidConverter());
            SerializerSettings.Converters.Add(new DateTimeConverter());
            SerializerSettings.Converters.Add(new DecimalConverter());
            SerializerSettings.Converters.Add(new StringEnumConverter());
        }

    }
}
