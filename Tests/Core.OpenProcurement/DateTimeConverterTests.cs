using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kapitalist.Core.OpenProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tests.Kapitalist.Core.OpenProcurement
{
	[TestClass()]
	public class DateTimeConverterTests
	{
		[TestMethod()]
		public void ReadDateTime()
		{
			DateTimeContainer data = JsonConvert.DeserializeObject<DateTimeContainer>(
				"{\"ComplexDate\":{},\"Time1\":\"0001-01-02T00:00:00+02:00\",\"Time2\":\"0001-01-01T02:00:00.0010000+02:00\"}",
				Settings.SerializerSettings);
			Assert.AreEqual(data, new DateTimeContainer {
				Time1 = DateTime.MinValue.AddDays(1).ToUniversalTime(),
				Time2 = DateTime.MinValue.AddMilliseconds(1),
				ComplexDate = new DateTimeContainer()
			});
		}

		[TestMethod()]
		public void WriteDateTime()
		{
			DateTimeContainer data = new DateTimeContainer {
				Time1 = DateTime.MinValue.AddDays(1).ToUniversalTime(),
				Time2 = DateTime.MinValue.AddMilliseconds(1),
				ComplexDate = new DateTimeContainer()
			};
			string content = JsonConvert.SerializeObject(data, Formatting.None, Settings.SerializerSettings);
			Assert.AreEqual(content,
				"{\"ComplexDate\":{},\"Time1\":\"0001-01-02T00:00:00+02:00\",\"Time2\":\"0001-01-01T02:00:00.0010000+02:00\"}");
		}

		public class DateTimeContainer
		{
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
			public DateTime Time1 { get; set; }
			public DateTime? Time2 { get; set; }
			public DateTimeContainer ComplexDate;

			public override bool Equals(object obj)
			{
				DateTimeContainer o = obj as DateTimeContainer;
				bool b = Time1.Equals(o.Time1);
				b = Time2.Equals(o.Time2);
				return o != null
					&& Time1.Equals(o.Time1)
					&& Time2.Equals(o.Time2)
					&& Equals(ComplexDate, o.ComplexDate);
			}

			public override int GetHashCode()
			{
				return 0;
			}
		}
	}
}