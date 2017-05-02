using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Store.Models
{
    public class SyncState
    {
        [Key]
        public SyncItems Type { get; set; }

        public bool Restoring { get; set; }
    }
}
