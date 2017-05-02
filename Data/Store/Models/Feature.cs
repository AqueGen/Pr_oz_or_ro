using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Feature : BaseFeature, IFeature
	{
		public Feature()
		{
		}

		public Feature(IFeature feature)
			: base(feature)
		{
		}

		public int Id { get; set; }

		[Required]
		public int TenderId { get; set; }

		public virtual Tender Tender { get; set; }

		public virtual ICollection<FeatureValue> Values { get; set; }
	}
}
