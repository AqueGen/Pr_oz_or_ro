using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kapitalist.Data.Store.Models
{
	[ComplexType]
	public class MultilingualText
	{
		public string Uk { get; set; }
		public string En { get; set; }
		public string Ru { get; set; }

		public override string ToString()
		{
			switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName) {
				case "en":
					return En;
				case "ru":
					return Ru;
				default:
					return Uk;
			}
		}

		public static implicit operator string(MultilingualText multilingualText) {
			return multilingualText.ToString();
		}
	}
}
