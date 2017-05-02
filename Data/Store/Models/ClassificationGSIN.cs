using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;

namespace Kapitalist.Data.Store.Models
{
	[Table("GSIN")]
	public class ClassificationGSIN : ClassificationMultilingual
	{
		[Required]
		[StringLength(14)]
		public override string Id { get; set; }
	}
}
