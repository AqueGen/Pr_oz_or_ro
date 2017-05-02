using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class DraftFeatureValue : BaseFeatureValue, IFeatureValue
	{
		public DraftFeatureValue()
		{
		}

		public DraftFeatureValue(IFeatureValue featureValue)
			: base(featureValue)
		{
		}

		public int Id { get; set; }

		public int FeatureId { get; set; }

		public virtual DraftFeature Feature { get; set; }
	}
}
