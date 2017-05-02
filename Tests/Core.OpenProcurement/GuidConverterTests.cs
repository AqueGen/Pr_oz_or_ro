using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Tests.Kapitalist.Core.OpenProcurement
{
	[TestClass]
	public class GuidConverterTests
	{
		[TestMethod]
		public void ReadGuid()
		{
			GuidContainer data = JsonConvert.DeserializeObject<GuidContainer>(
				"{\"Guid1\":\"00000000000000000000000000000000\",\"Guid2\":\"10000000000000000000000000000000\",\"ComplexGuid\":{}}",
				Settings.SerializerSettings);
			Assert.AreEqual(data, new GuidContainer {
				Guid2 = new Guid("10000000000000000000000000000000"),
				ComplexGuid = new GuidContainer()
			});
		}

		[TestMethod]
		public void WriteGuid()
		{
			GuidContainer data = new GuidContainer {
				Guid1 = new Guid("00000000000000000000000000000000"),
				Guid2 = new Guid("10000000000000000000000000000000"),
				ComplexGuid = new GuidContainer()
			};
			string content = JsonConvert.SerializeObject(data, Formatting.None, Settings.SerializerSettings);
			Assert.AreEqual(content,
				"{\"Guid2\":\"10000000000000000000000000000000\",\"ComplexGuid\":{\"Guid2\":\"00000000000000000000000000000000\"}}");
		}

		class GuidContainer
		{
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
			public Guid Guid1;
			public Guid Guid2;
			public GuidContainer ComplexGuid;

			public override bool Equals(object obj)
			{
				GuidContainer o = obj as GuidContainer;
				return o != null
					&& Guid1.Equals(o.Guid1)
					&& Guid2.Equals(o.Guid2)
					&& Equals(ComplexGuid, o.ComplexGuid);
			}

			public override int GetHashCode()
			{
				return 0;
			}
		}
	}
}
