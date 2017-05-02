using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Kapitalist.Web.Framework.ModelBinders
{
    public class BaseModelBinder : DefaultModelBinder
    {
        protected class Request
        {
            private readonly NameValueCollection _request;
            private const string DateTimeFormat = "dd.MM.yyyy HH:mm";

            public Request(ControllerContext controllerContext)
            {
                if (controllerContext.HttpContext.Request.HttpMethod == "GET")
                {
                    _request = controllerContext.HttpContext.Request.QueryString;
                }
                else
                {
                    _request = controllerContext.HttpContext.Request.Form;
                }
            }

            public DateTime? ToDateTime(string name, string dateFormat = DateTimeFormat)
            {
                var culture = CultureInfo.InvariantCulture;
                return ToDateTime(name, dateFormat, culture);
            }

            public DateTime? ToDateTime(string name, string dateFormat, CultureInfo culture)
            {
                string textDateTime = GetValue<string>(name);

                if (string.IsNullOrWhiteSpace(textDateTime))
                    return (DateTime?) null;

                var dateTime = DateTime.ParseExact(textDateTime, dateFormat, culture);
                return dateTime;
            }


            public List<DateTime?> ToDateTimes(string name, string dateFormat = DateTimeFormat)
            {
                var culture = CultureInfo.InvariantCulture;
                return ToDateTimes(name, dateFormat, culture);
            }

            public List<DateTime?> ToDateTimes(string name, string dateFormat, CultureInfo culture)
            {
                var textList = GetValues<string>(name);
                if (textList == null)
                    return null;
                var dateTimes = new List<DateTime?>();
                foreach (var item in textList)
                {
                    DateTime? dateTime = ToDateTime(name, dateFormat, culture);
                    dateTimes.Add(dateTime);
                }
                return dateTimes;
            }


            public T GetValue<T>(string name)
            {
                string[] array = RequestValues(name);

                if (array == null)
                    return default(T);

                return ConverterValue<T>(array[0]);
            }

            public List<T> GetValues<T>(string name)
            {
                string[] array = RequestValues(name);
                if (array == null)
                    return null;

                return ConverterList<T>(array);
            }


            private string[] RequestValues(string name)
            {
                return _request.GetValues(name);
            }

            private T ConverterValue<T>(string value)
            {
                if (typeof(T) == typeof(int))
                {
                    return (T) (object) Convert.ToInt32(value);
                }
                if (typeof(T) == typeof(long))
                {
                    return (T) (object) Convert.ToInt64(value);
                }
                if (typeof(T) == typeof(bool))
                {
                    return (T)(object)Convert.ToBoolean(value);
                }
                if (typeof(T) == typeof(double))
                {
                    return (T) (object) Convert.ToDouble(value);
                }
                if (typeof(T) == typeof(decimal))
                {
                    return (T) (object) Convert.ToDecimal(value);
                }
                return (T) (object) value;
            }

            private List<T> ConverterList<T>(string[] array)
            {
                return array.Select(ConverterValue<T>).ToList();
            }
        }
    }
}