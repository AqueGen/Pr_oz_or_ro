using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Store.Models
{
	public class SyncError
	{
        [Key]
        [Column(Order = 0)]
        public SyncItems Type { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid Guid { get; set; }

        public DateTime Offset { get; set; }
	}
}
