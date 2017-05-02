using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class FeatureValue : BaseFeatureValue, IFeatureValue
	{
		public FeatureValue()
		{
		}

		public FeatureValue(IFeatureValue featureValue)
			: base(featureValue)
		{
		}

		public int Id { get; set; }

		public int FeatureId { get; set; }

		public virtual Feature Feature { get; set; }
	}
}
